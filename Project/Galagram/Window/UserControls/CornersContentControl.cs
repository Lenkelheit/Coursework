using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Galagram.Window.UserControls
{
    /// <summary>
    /// Represents a control content control with dotted line
    /// </summary>
    public class CornersContentControl : ContentControl
    {
        // CONSTRUCTORS
        /// <summary>
        /// Initialize a new instance of <see cref="CornersContentControl"/>
        /// </summary>
        public CornersContentControl()
        {
            SnapsToDevicePixels = true;
            UseLayoutRounding = true;
        }

        // DEPENDECY PROPERTIES

        // STROKE
        #region Stroke
        /// <summary>
        /// Dependency property for <see cref="Stroke"/>
        /// </summary>
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            name: nameof(Stroke),
            propertyType: typeof(Brush),
            ownerType: typeof(CornersContentControl), 
            typeMetadata: new PropertyMetadata(defaultValue: default(Brush), propertyChangedCallback: OnVisualPropertyChanged));

        /// <summary>
        /// Gets or sets stroke color
        /// </summary>
        public Brush Stroke
        {
            get
            {
                return (Brush)GetValue(StrokeProperty);
            }
            set
            {
                SetValue(StrokeProperty, value);
            }
        }
        #endregion

        // STROKE THICKNESS
        #region StrokeThickness
        /// <summary>
        /// Dependency property for <see cref="StrokeThickness"/>
        /// </summary>
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            name: nameof(StrokeThickness),
            propertyType: typeof(double),
            ownerType: typeof(CornersContentControl),
            typeMetadata: new PropertyMetadata(defaultValue: default(double), propertyChangedCallback: OnVisualPropertyChanged));

        /// <summary>
        /// Gets or sets stroke thickness
        /// </summary>
        public double StrokeThickness
        {
            get
            {
                return (double)GetValue(StrokeThicknessProperty);
            }
            set
            {
                SetValue(StrokeThicknessProperty, value);
            }
        }
        #endregion

        // STROKE DASH LINE
        #region StrokeDashLine
        /// <summary>
        /// Dependency property for <see cref="StrokeDashLine"/>
        /// </summary>
        public static readonly DependencyProperty StrokeDashLineProperty = DependencyProperty.Register(
            name: nameof(StrokeDashLine),
            propertyType: typeof(double),
            ownerType: typeof(CornersContentControl),
            typeMetadata: new PropertyMetadata(defaultValue: default(double), propertyChangedCallback: OnVisualPropertyChanged));
        
        /// <summary>
        /// Gets or sets stroke dash line width
        /// </summary>
        public double StrokeDashLine
        {
            get
            {
                return (double)GetValue(StrokeDashLineProperty);
            }
            set
            {
                SetValue(StrokeDashLineProperty, value);
            }
        }
        #endregion

        // STROKE DASH SPACE
        #region StrokeDashSpace
        /// <summary>
        /// Dependency property <see cref="StrokeDashSpace"/>
        /// </summary>
        public static readonly DependencyProperty StrokeDashSpaceProperty = DependencyProperty.Register(
            name: nameof(StrokeDashSpace),
            propertyType: typeof(double),
            ownerType: typeof(CornersContentControl),
            typeMetadata: new PropertyMetadata(defaultValue: default(double), propertyChangedCallback: OnVisualPropertyChanged));
       
        /// <summary>
        /// Gets or sets stroke dash space
        /// </summary>
        public double StrokeDashSpace
        {
            get
            {
                return (double)GetValue(StrokeDashSpaceProperty);
            }
            set
            {
                SetValue(StrokeDashSpaceProperty, value);
            }
        }
        #endregion

        // FILL
        #region Fill
        /// <summary>
        /// Dependency property for <see cref="Fill"/>
        /// </summary>
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            name: nameof(Fill),
            propertyType: typeof(Brush),
            ownerType: typeof(CornersContentControl),
            typeMetadata: new PropertyMetadata(defaultValue: default(Brush), propertyChangedCallback: OnVisualPropertyChanged));
        
        /// <summary>
        /// Gets or sets fill color
        /// </summary>
        public Brush Fill
        {
            get
            {
                return (Brush)GetValue(FillProperty);
            }
            set
            {
                SetValue(FillProperty, value);
            }
        }
        #endregion

        // METHODS
        /// <summary>
        /// Participates in rendering operations that
        /// are directed by the layout system. The rendering instructions for this element
        /// are not used directly when this method is invoked, and are instead preserved
        /// for later asynchronous use by layout and drawing.
        /// </summary>
        /// <param name="drawingContext">
        /// The drawing instructions for a specific element. This context is provided to the layout system.
        /// </param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            double w = ActualWidth;
            double h = ActualHeight;
            double x = StrokeThickness / 2.0;

            Pen horizontalPen = GetPen(ActualWidth - 2.0 * x);
            Pen verticalPen = GetPen(ActualHeight - 2.0 * x);

            drawingContext.DrawRectangle(Fill, null, new Rect(new Point(0, 0), new Size(w, h)));

            drawingContext.DrawLine(horizontalPen, new Point(x, x), new Point(w - x, x));
            drawingContext.DrawLine(horizontalPen, new Point(x, h - x), new Point(w - x, h - x));

            drawingContext.DrawLine(verticalPen, new Point(x, x), new Point(x, h - x));
            drawingContext.DrawLine(verticalPen, new Point(w - x, x), new Point(w - x, h - x));
        }

        private Pen GetPen(double length)
        {
            IEnumerable<double> dashArray = GetDashArray(length);

            return new Pen(Stroke, StrokeThickness)
            {
                DashStyle = new DashStyle(dashArray, 0),
                EndLineCap = PenLineCap.Square,
                StartLineCap = PenLineCap.Square,
                DashCap = PenLineCap.Flat
            };
        }
        private IEnumerable<double> GetDashArray(double length)
        {
            double useableLength = length - StrokeDashLine;
            int lines = (int)Math.Round(useableLength / (StrokeDashLine + StrokeDashSpace));
            useableLength -= lines * StrokeDashLine;
            double actualSpacing = useableLength / lines;

            yield return StrokeDashLine / StrokeThickness;
            yield return actualSpacing / StrokeThickness;
        }
        private static void OnVisualPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CornersContentControl)d).InvalidateVisual();
        }

    }
}
