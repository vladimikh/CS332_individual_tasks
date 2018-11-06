using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Individual_task_1
{
    public partial class Form1 : Form
    {
        private LinkedList<PointFlagLink>[] Polygons =
        {
            new LinkedList<PointFlagLink>(),
            new LinkedList<PointFlagLink>(),
        };

        public class PointFlagLink
        {
            public PointF p;
            public bool isIntersection;
            public float angle;
            public LinkedListNode<PointFlagLink> otherNode;

            public PointFlagLink(PointF _p, bool _is_intersecton = false, float _angle = 0)
            {
                p = _p;
                isIntersection = _is_intersecton;
                angle = _angle;
            }
        }

        List<PointF> Union;

        public Form1()
        {
            InitializeComponent();
            initPolygonsWithRandomValues();
        }

        private bool new_points = false;

        void initPolygonsWithRandomValues()
        {
            new_points = true;

            /*
            Polygons[0] = new LinkedList<PointFlagLink>(new PointFlagLink[]
            {
                new PointFlagLink(new PointF(0, 200)),
                new PointFlagLink(new PointF(100, 600)),
                new PointFlagLink(new PointF(1100, 900)),
                new PointFlagLink(new PointF(1000, 0)),
            });

            Polygons[1] = new LinkedList<PointFlagLink>(new PointFlagLink[]
           {
                new PointFlagLink(new PointF(300, 0)),
                new PointFlagLink(new PointF(200, 900)),
                new PointFlagLink(new PointF(1300, 600)),
                new PointFlagLink(new PointF(1200, 200)),

           });
           */

            /*
            //for testing
            Polygons[0] = new LinkedList<PointFlagLink>(new PointFlagLink[]
            {
                new PointFlagLink(new PointF(200, 200)),
                new PointFlagLink(new PointF(200, 600)),
                new PointFlagLink(new PointF(600, 600)),
                new PointFlagLink(new PointF(600, 200)),
            });

            Polygons[1] = new LinkedList<PointFlagLink>(new PointFlagLink[]
            {
                new PointFlagLink(new PointF(500, 500)),
                new PointFlagLink(new PointF(500, 700)),
                new PointFlagLink(new PointF(700, 700)),
                new PointFlagLink(new PointF(700, 500)),

            });
             */
            /*
           Polygons[0] = new LinkedList<PointFlagLink>(new PointFlagLink[]
               {
                   new PointFlagLink(new PointF(316, 427)),
                   new PointFlagLink(new PointF(905, 492)),
                   new PointFlagLink(new PointF(1170, 892)),
                   new PointFlagLink(new PointF(256, 854)),
               });
           Polygons[1] = new LinkedList<PointFlagLink>(new PointFlagLink[]
               {
                   new PointFlagLink(new PointF(129, 423)),
                   new PointFlagLink(new PointF(894, 111)),
                   new PointFlagLink(new PointF(1077, 942)),
                   new PointFlagLink(new PointF(476, 643)),
               });

          */
            
             Random rand = new Random();

             for (int i = 0; i < 2; ++i)
             {
                 Polygons[i] = new LinkedList<PointFlagLink>(new PointFlagLink[]
                 {
                     new PointFlagLink(new PointF(rand.Next(50, 500), rand.Next(50, 500))),
                     new PointFlagLink(new PointF(rand.Next(800, 1250), rand.Next(50, 500))),
                     new PointFlagLink(new PointF(rand.Next(800, 1250), rand.Next(550, 950))),
                     new PointFlagLink(new PointF(rand.Next(50, 500), rand.Next(600, 900))),
                 });

             }

            canvas_pictureBox.Refresh();
        }

        private void rand_button_Click(object sender, EventArgs e)
        {
            initPolygonsWithRandomValues();
        }

        /*
        private float signed_polygon_area(List<PointFlagLink> points)
        {
            // Adding the first point to the end
            int num_points = points.Count;
            PointFlagLink[] pts = new PointFlagLink[num_points + 1];
            points.CopyTo(pts, 0);
            pts[num_points] = points[0];

            // Get the areas
            float area = 0;
            for (int i = 0; i < num_points; ++i)
            {
                area += pts[i].p.X * pts[i + 1].p.Y - pts[i+1].p.X * pts[i].p.Y;
            }

            // Return the result.
            return area;
        }
        */
        /*
        PointF find_barycenter(List<PointFlagLink> points, float area)
        {
            float gx_sum = 0f, gy_sum = 0f;
            for (int i = 0; i < points.Count - 1; ++i)
            {
                float xi = points[i].p.X;
                float yi = points[i].p.Y;
                float xi_next = points[i + 1].p.X;
                float yi_next = points[i + 1].p.Y;

                gx_sum += (xi + xi_next) * (xi * yi_next - xi_next * yi);
                gy_sum += (yi + yi_next) * (xi * yi_next - xi_next * yi);
            }
            float coef = 1 / (6 * area);
            PointF G = new PointF(coef*gx_sum, coef*gy_sum);
            return G;
        }
        */

        private float findAngle(float y, float x, PointF avgCenter)
        {
            return (float)Math.Atan2(y - avgCenter.Y, x - avgCenter.X);
        }

        private void sortPointsClockwise(ref LinkedList<PointFlagLink> pointArr, out PointF avgCenter)
        {
            float cx = pointArr.Average(el => el.p.X);
            float cy = pointArr.Average(el => el.p.Y);

            avgCenter = new PointF(cx, cy); 

            for (var curNode = pointArr.First; curNode != null; curNode = curNode.Next)
            {
                curNode.Value.angle = findAngle(curNode.Value.p.Y, curNode.Value.p.X, avgCenter);
            }

            LinkedList<PointFlagLink> tempLinkedList = new LinkedList<PointFlagLink>(pointArr);
            pointArr.Clear();
            IEnumerable<PointFlagLink> orderedEnumerable = tempLinkedList.OrderByDescending(p => p.angle).AsEnumerable();
            foreach (var oe in orderedEnumerable)
            {
                pointArr.AddLast(oe);
            }
        }

        /*
           float A = signed_polygon_area(point_arr);
           if (A < 0)
           {
               return;
           }
           PointF barycenter = find_barycenter(point_arr, A);

           for(int i = 0; i < point_arr.Count; ++i)
           {
               point_arr[i].angle = (float)Math.Atan2(point_arr[i].p.Y - barycenter.Y, point_arr[i].p.X - barycenter.X);
           }
           point_arr = point_arr.OrderBy(p => p.angle).ToList();
           */

        void getEquationCoeffs(PointF a, PointF b, out float aa, out float bb, out float cc) {
            aa = a.Y - b.Y;
            bb = b.X - a.X;
            cc = a.X * b.Y - b.X * a.Y;
        }

        bool isBetweenCoords(float z, float z1, float z2)
        {
            return Math.Min(z1, z2) - float.Epsilon <= z && z <= Math.Max(z1, z2) + float.Epsilon;
        }

        bool isBeetweenSegmentsEnds(float x, float y, PointF a1, PointF b1, PointF a2, PointF b2)
        {
            return isBetweenCoords(x, a1.X, b1.X) &&
                   isBetweenCoords(x, a2.X, b2.X) &&
                   isBetweenCoords(y, a1.Y, b1.Y) &&
                   isBetweenCoords(y, a2.Y, b2.Y);
        }

        bool areEqual(float a, float b) 
        {
	        return Math.Abs(a - b) < float.Epsilon;
        }

        bool isIntersection(PointF a1, PointF b1, PointF a2, PointF b2, out PointF intersection_point)
        {
            float aa1, bb1, cc1, aa2, bb2, cc2;
            getEquationCoeffs(a1, b1, out aa1, out bb1, out cc1);
            getEquationCoeffs(a2, b2, out aa2, out bb2, out cc2);

            //if lines are parallel
            if (areEqual(aa1 * bb2, aa2 * bb1))
            {
                intersection_point = new PointF();
                return false;
            }

            //x and y of a potential intersection point
            float x = (cc2 * bb1 - cc1 * bb2) / (aa1 * bb2 - aa2 * bb1);
            float y = (cc2 * aa1 - cc1 * aa2) / (bb1 * aa2 - bb2 * aa1);

            if (isBeetweenSegmentsEnds(x, y, a1, b1, a2, b2))
            {
                intersection_point = new PointF(x, y);
                return true;
            }
            else
            {
                intersection_point = new PointF();
                return false;
            }
        }

        private float distance(PointF p1, PointF p2)
        {
            return (float)Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
        }

        private void addAllIntersections (ref LinkedList<PointFlagLink>[] polygons)
        {
            PointF pol1center, pol2center;
            sortPointsClockwise(ref polygons[0], out pol1center);
            sortPointsClockwise(ref polygons[1], out pol2center);

            var pol1 = new PointFlagLink[polygons[0].Count + 1];
            var pol2 = new PointFlagLink[polygons[1].Count + 1];

            polygons[0].CopyTo(pol1, 0);
            polygons[1].CopyTo(pol2, 0);

            //LinkedList<PointFlagLink> pol1 = polygons[0].ConvertAll(el => new PointFlagLink(el.p, el.is_intersection, el.angle, el.other_point));
            //LinkedList<PointFlagLink> pol2 = polygons[1].ConvertAll(el => new PointFlagLink(el.p, el.is_intersection, el.angle, el.other_point));

            pol1[polygons[0].Count] = pol1.First();
            pol2[polygons[1].Count] = pol2.First();

            for (int i = 0; i < pol1.Count() - 1; ++i)
            {
                for(int j = 0; j < pol2.Count() - 1; ++j)
                {
                    PointF intersection_point;
                    if (isIntersection(pol1[i].p, pol1[i+1].p, pol2[j].p, pol2[j + 1].p, out intersection_point))
                    {
                        PointFlagLink ip1 = new PointFlagLink(intersection_point, true);
                        // find_angle(intersection_point.Y, intersection_point.X, pol1center));
                        PointFlagLink ip2 = new PointFlagLink(intersection_point, true);
                                                             // find_angle(intersection_point.Y, intersection_point.X, pol2center));

                        var t1 = polygons[0].Find(pol1[i]);
                        while (t1.Next != null && t1.Next.Value.isIntersection && distance(pol1[i+1].p, t1.Next.Value.p) > distance(pol1[i+1].p, ip1.p))
                            t1 = t1.Next;
                        var n1 = polygons[0].AddAfter(t1, ip1);
                        
                        var t2 = polygons[1].Find(pol2[j]);
                        while (t2.Next != null && t2.Next.Value.isIntersection && distance(pol2[j+1].p, t2.Next.Value.p) > distance(pol2[j+1].p, ip2.p))
                            t2 = t2.Next;
                        var n2 = polygons[1].AddAfter(t2, ip2);

                        n1.Value.otherNode = n2;
                        n2.Value.otherNode = n1;

                    }
                }
            }
        }

        // Return the union of the two polygons
        private List<PointF> find_polygon_union(ref LinkedList<PointFlagLink>[] polygons)
        {
            addAllIntersections(ref polygons);

            // Just the first element
            var cur_node = polygons[0].First;

            //leftmost, uppermost
            for (int pgon = 0; pgon < 2; ++pgon)
            {
                for (var test_node = polygons[pgon].First; test_node != null; test_node = test_node.Next)
                {
                    PointF test_point = test_node.Value.p;
                    if ((test_point.X < cur_node.Value.p.X) ||
                        ((test_point.X == cur_node.Value.p.X) &&
                         (test_point.Y > cur_node.Value.p.Y)))
                    {
                        cur_node = test_node;
                    }
                }
            }
            
            // Create the result polygon.
            List<PointF> union = new List<PointF>();

            PointF start_point = cur_node.Value.p;

            while (true)
            {
                union.Add(cur_node.Value.p);

                var next_node = cur_node.Next ?? cur_node.List.First;

                if (next_node.Value.p == start_point)
                {
                    break;
                }

                if (next_node.Value.isIntersection)
                {
                    cur_node = next_node.Value.otherNode;
                }
                else
                {
                    cur_node = next_node;
                }
            }

            return union;
        }

        // The polygons' colors.
        private Color[] PolygonColors =
        {
            Color.Tomato,
            Color.SkyBlue
        };

        private void DrawPolygons(PaintEventArgs e)
        {
            // Draw the polygons
            for (int i = 0; i < 2; ++i)
            {
                // See if we are making this polygon
                if (i == building_index)
                {
                    // We are. Draw the segments
                    if (Polygons[i].Count > 1)
                        using (Pen pen = new Pen(PolygonColors[i], 3))
                        {
                            e.Graphics.DrawLines(pen,
                                Polygons[i].Select(el => el.p).ToArray());
                        }

                    // Draw to the mouse's current location
                    if (Polygons[i].Count > 0)
                    {
                        PointF point1 = Polygons[i].Last().p;
                        e.Graphics.DrawLine(Pens.Green,
                            point1.X, point1.Y,
                            CurrentCursorLocation.X, CurrentCursorLocation.Y);
                    }
                }
                // We're not making this polygon. Draw it.
                if (Polygons[i].Count > 2)
                {
                    Color fill_color = Color.FromArgb(128, PolygonColors[i]);
                    using (Pen pen = new Pen(PolygonColors[i], 3))
                    {
                        e.Graphics.DrawPolygon(pen,
                           Polygons[i].Select(el => el.p).ToArray());
                    }
                    using (Brush brush = new SolidBrush(fill_color))
                    {
                        e.Graphics.FillPolygon(brush,
                            Polygons[i].Select(el => el.p).ToArray());
                    }
                }
            }
        }

        private PointF CurrentCursorLocation;

        private int building_index = -1;

        // Draw the polygons and their union
        private void canvas_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(canvas_pictureBox.BackColor);
        
            DrawPolygons(e);

            // If we have both polygons, draw the union
            if ((building_index == -1) &&
                (Polygons[0].Count > 2) &&
                (Polygons[1].Count > 2) &&
                show_union_checkBox.Checked)
            {
                if (new_points)
                {
                    Union = find_polygon_union(ref Polygons);
                    new_points = false;
                }

                using (Pen pen = new Pen(Color.Black, 10))
                {
                    e.Graphics.DrawPolygon(pen, Union.ToArray());
                }
            }
        }

        private void show_union_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            canvas_pictureBox.Refresh();
        }

        private void canvas_pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (building_index < 0)
            {
                // Start a new polygon
                building_index = e.Button == MouseButtons.Left? 0 : 1;

                Polygons[building_index] = new LinkedList<PointFlagLink>();
                Polygons[building_index].AddLast(new PointFlagLink (e.Location));
                CurrentCursorLocation = e.Location;
            }
            else
            {
                // Add a new point to the current new polygon
                if (Polygons[building_index].Last().p != e.Location)
                    Polygons[building_index].AddLast(new PointFlagLink(e.Location));
                CurrentCursorLocation = e.Location;
            }

            canvas_pictureBox.Refresh();
        }

        private void canvas_pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (building_index < 0) return;
            CurrentCursorLocation = e.Location;
            canvas_pictureBox.Refresh();
        }

        private void canvas_pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Polygons[building_index].Count < 3)
            {
                Polygons[building_index] = new LinkedList<PointFlagLink>();
            }

            // Building is finished

            building_index = -1;
            new_points = true;
            canvas_pictureBox.Refresh();
        }

    }
}
