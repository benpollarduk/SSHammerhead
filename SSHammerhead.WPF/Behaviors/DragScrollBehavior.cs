using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SSHammerhead.WPF.Behaviors
{
    /// <summary>
    /// Provides an attached behavior that enables click-and-drag scrolling for a control that contains a ScrollViewer.
    /// This allows the content to be scrolled by dragging anywhere within the control, even when the scroll bar is
    /// missed or visually distorted by effects.
    /// </summary>
    public static class DragScrollBehavior
    {
        #region Fields

        private static readonly DependencyProperty TrackerProperty = DependencyProperty.RegisterAttached("Tracker", typeof(DragScrollTracker), typeof(DragScrollBehavior), new PropertyMetadata(null));

        #endregion

        #region AttachedProperties

        /// <summary>
        /// Identifies the DragScrollBehavior.IsEnabled attached property.
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(DragScrollBehavior), new PropertyMetadata(false, OnIsEnabledChanged));

        /// <summary>
        /// Get the DragScrollBehavior.IsEnabled property for a specified element.
        /// </summary>
        /// <param name="element">The element to get the property for.</param>
        /// <returns>True if drag scrolling is enabled, else false.</returns>
        public static bool GetIsEnabled(DependencyObject element)
        {
            return (bool)element.GetValue(IsEnabledProperty);
        }

        /// <summary>
        /// Set the DragScrollBehavior.IsEnabled property for a specified element.
        /// </summary>
        /// <param name="element">The element to set the property for.</param>
        /// <param name="value">The value to set.</param>
        public static void SetIsEnabled(DependencyObject element, bool value)
        {
            element.SetValue(IsEnabledProperty, value);
        }

        #endregion

        #region Methods

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement element)
                return;

            if ((bool)e.NewValue)
            {
                var tracker = new DragScrollTracker(element);
                element.SetValue(TrackerProperty, tracker);
            }
            else if (element.GetValue(TrackerProperty) is DragScrollTracker tracker)
            {
                tracker.Detach();
                element.ClearValue(TrackerProperty);
            }
        }

        #endregion

        #region DragScrollTracker

        /// <summary>
        /// Tracks the drag state and wires up the mouse handling for a host element.
        /// </summary>
        private sealed class DragScrollTracker
        {
            #region Fields

            private readonly FrameworkElement host;
            private ScrollViewer? scrollViewer;
            private Point lastPosition;
            private double verticalOffsetAtStart;
            private double horizontalOffsetAtStart;
            private bool isDragging;

            #endregion

            #region Constructors

            public DragScrollTracker(FrameworkElement host)
            {
                this.host = host;

                if (host.IsLoaded)
                    Attach();
                else
                    host.Loaded += OnHostLoaded;
            }

            #endregion

            #region Methods

            private void OnHostLoaded(object sender, RoutedEventArgs e)
            {
                host.Loaded -= OnHostLoaded;
                Attach();
            }

            private void Attach()
            {
                scrollViewer = FindDescendant<ScrollViewer>(host);

                // force the default arrow cursor and disable text selection so the terminal does not behave like editable text.
                DisableTextSelection();

                host.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
                host.PreviewMouseMove += OnPreviewMouseMove;
                host.PreviewMouseLeftButtonUp += OnPreviewMouseLeftButtonUp;
                host.MouseLeave += OnMouseLeave;
            }

            private void DisableTextSelection()
            {
                // always show the default arrow cursor over the terminal, overriding the I-beam from the document viewer.
                host.Cursor = Cursors.Arrow;
                host.ForceCursor = true;

                // the markup terminal hosts its content in a FlowDocumentScrollViewer, which allows text selection.
                var documentViewer = FindDescendant<FlowDocumentScrollViewer>(host);

                if (documentViewer != null)
                {
                    documentViewer.IsSelectionEnabled = false;
                    documentViewer.Cursor = Cursors.Arrow;
                    documentViewer.ForceCursor = true;
                }
            }

            public void Detach()
            {
                host.Loaded -= OnHostLoaded;
                host.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
                host.PreviewMouseMove -= OnPreviewMouseMove;
                host.PreviewMouseLeftButtonUp -= OnPreviewMouseLeftButtonUp;
                host.MouseLeave -= OnMouseLeave;
                scrollViewer = null;
            }

            private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                // the scroll viewer may not have been realized when the host first loaded, so resolve it lazily.
                scrollViewer ??= FindDescendant<ScrollViewer>(host);

                if (scrollViewer == null)
                    return;

                // nothing to scroll, so don't interfere with normal interaction.
                if (scrollViewer.ScrollableHeight <= 0 && scrollViewer.ScrollableWidth <= 0)
                    return;

                lastPosition = e.GetPosition(host);
                verticalOffsetAtStart = scrollViewer.VerticalOffset;
                horizontalOffsetAtStart = scrollViewer.HorizontalOffset;
                isDragging = true;
            }

            private void OnPreviewMouseMove(object sender, MouseEventArgs e)
            {
                if (!isDragging || scrollViewer == null)
                    return;

                if (e.LeftButton != MouseButtonState.Pressed)
                {
                    EndDrag();
                    return;
                }

                var currentPosition = e.GetPosition(host);
                var deltaX = currentPosition.X - lastPosition.X;
                var deltaY = currentPosition.Y - lastPosition.Y;

                // once the user has moved beyond a small threshold, begin capturing and treat this as a drag scroll.
                if (!host.IsMouseCaptured && Math.Abs(deltaX) < SystemParameters.MinimumHorizontalDragDistance && Math.Abs(deltaY) < SystemParameters.MinimumVerticalDragDistance)
                    return;

                if (!host.IsMouseCaptured)
                    host.CaptureMouse();

                scrollViewer.ScrollToVerticalOffset(verticalOffsetAtStart + deltaY);
                scrollViewer.ScrollToHorizontalOffset(horizontalOffsetAtStart + deltaX);

                e.Handled = true;
            }

            private void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
            {
                EndDrag();
            }

            private void OnMouseLeave(object sender, MouseEventArgs e)
            {
                if (!host.IsMouseCaptured)
                    EndDrag();
            }

            private void EndDrag()
            {
                if (host.IsMouseCaptured)
                    host.ReleaseMouseCapture();

                isDragging = false;
            }

            private static T? FindDescendant<T>(DependencyObject root) where T : DependencyObject
            {
                var count = VisualTreeHelper.GetChildrenCount(root);

                for (var i = 0; i < count; i++)
                {
                    var child = VisualTreeHelper.GetChild(root, i);

                    if (child is T match)
                        return match;

                    var descendant = FindDescendant<T>(child);

                    if (descendant != null)
                        return descendant;
                }

                return null;
            }

            #endregion
        }

        #endregion
    }
}
