using System;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Assets.Players.SpiderBot.FrameBuilders.Console
{
    /// <summary>
    /// Provides a console room map builder.
    /// </summary>
    /// <param name="gridStringBuilder">The string builder to use.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class BotConsoleRoomMapBuilder(GridStringBuilder gridStringBuilder, ConsoleMapRenderProperties properties) : IConsoleRoomMapBuilder
    {
        #region StaticProperties

        /// <summary>
        /// Get the maximum size of the key.
        /// </summary>
        public static readonly Size MaximumKeySize = new(25, 4);

        #endregion

        #region Methods

        /// <summary>
        /// Draw the north border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawNorthBorder(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            gridStringBuilder.SetCell(startPosition.X, startPosition.Y, properties.Corner, properties.BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 1, startPosition.Y, properties.HorizontalBoundary, properties.BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y, properties.VerticalExitBorder, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, properties.LockedExit, properties.LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y, properties.VerticalExitBorder, properties.BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y, properties.VerticalExitBorder, properties.BoundaryColor);

                if (viewPoint[Direction.North]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, 'n', properties.VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, 'N', properties.UnvisitedExitColor);

                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y, properties.VerticalExitBorder, properties.BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 3, startPosition.Y, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 5, startPosition.Y, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y, properties.HorizontalBoundary, properties.BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X + 7, startPosition.Y, properties.HorizontalBoundary, properties.BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y, properties.Corner, properties.BoundaryColor);
        }

        /// <summary>
        /// Draw the south border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawSouthBorder(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 6, properties.Corner, properties.BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 1, startPosition.Y + 6, properties.HorizontalBoundary, properties.BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 6, properties.VerticalExitBorder, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, properties.LockedExit, properties.LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 6, properties.VerticalExitBorder, properties.BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 6, properties.VerticalExitBorder, properties.BoundaryColor);

                if (viewPoint[Direction.South]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, 's', properties.VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, 'S', properties.UnvisitedExitColor);

                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 6, properties.VerticalExitBorder, properties.BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 6, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 3, startPosition.Y + 6, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 5, startPosition.Y + 6, properties.HorizontalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 6, properties.HorizontalBoundary, properties.BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X + 7, startPosition.Y + 6, properties.HorizontalBoundary, properties.BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 6, properties.Corner, properties.BoundaryColor);
        }

        /// <summary>
        /// Draw the east border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawEastBorder(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 1, properties.VerticalBoundary, properties.BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.East))
            {
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 2, properties.HorizontalExitBorder, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, properties.LockedExit, properties.LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 4, properties.HorizontalExitBorder, properties.BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.East))
            {
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 2, properties.HorizontalExitBorder, properties.BoundaryColor);

                if (viewPoint[Direction.East]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, 'e', properties.VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, 'E', properties.UnvisitedExitColor);

                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 4, properties.HorizontalExitBorder, properties.BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 2, properties.VerticalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, properties.VerticalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 4, properties.VerticalBoundary, properties.BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 5, properties.VerticalExitBorder, properties.BoundaryColor);
        }

        /// <summary>
        /// Draw the west border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawWestBorder(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 1, properties.VerticalBoundary, properties.BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.West))
            {
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 2, properties.HorizontalExitBorder, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, properties.LockedExit, properties.LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 4, properties.HorizontalExitBorder, properties.BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.West))
            {
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 2, properties.HorizontalExitBorder, properties.BoundaryColor);

                if (viewPoint[Direction.West]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, 'w', properties.VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, 'W', properties.UnvisitedExitColor);

                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 4, properties.HorizontalExitBorder, properties.BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 2, properties.VerticalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, properties.VerticalBoundary, properties.BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 4, properties.VerticalBoundary, properties.BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 5, properties.VerticalExitBorder, properties.BoundaryColor);
        }

        /// <summary>
        /// Draw the up exit.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawUpExit(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            if (room.HasLockedExitInDirection(Direction.Up))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 2, properties.LockedExit, properties.LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.Up))
            {
                if (viewPoint[Direction.Up]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 2, 'u', properties.VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 2, 'U', properties.UnvisitedExitColor);
            }
        }

        /// <summary>
        /// Draw the down exit.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawDownExit(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            if (room.HasLockedExitInDirection(Direction.Down))
            {
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 2, properties.LockedExit, properties.LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.Down))
            {
                if (viewPoint[Direction.Down]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 2, 'd', properties.VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 2, 'D', properties.UnvisitedExitColor);
            }
        }

        /// <summary>
        /// Draw the item or character.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawItemOrCharacter(Room room, Point2D startPosition)
        {
            if (Array.Exists(room.Items, x => x.IsPlayerVisible) || Array.Exists(room.Characters, x => x.IsPlayerVisible))
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 3, properties.PointOfInterest, properties.PointOfInterestColor);
        }

        #endregion

        #region Implementation of IRoomMapBuilder

        /// <inheritdoc/>
        public Size RenderedSize => new(9, 7);

        /// <inheritdoc/>
        public void BuildRoomMap(Room room, ViewPoint viewPoint, RoomMapRenderOptions options)
        {
            BuildRoomMap(room, viewPoint, options, new Point2D(0, 0), out _, out _);
        }

        #endregion

        #region Implementation of IConsoleRoomMapBuilder

        /// <inheritdoc/>
        public void BuildRoomMap(Room room, ViewPoint viewPoint, RoomMapRenderOptions options, Point2D startPosition, out int endX, out int endY)
        {
            DrawNorthBorder(room, viewPoint, startPosition);
            DrawSouthBorder(room, viewPoint, startPosition);
            DrawEastBorder(room, viewPoint, startPosition);
            DrawWestBorder(room, viewPoint, startPosition);
            DrawUpExit(room, viewPoint, startPosition);
            DrawDownExit(room, viewPoint, startPosition);
            DrawItemOrCharacter(room, startPosition);

            endX = 0;
            endY = startPosition.Y + 6;
        }

        /// <inheritdoc/>
        public Size Measure(Room room, ViewPoint viewPoint, RoomMapRenderOptions options)
        {
            return RenderedSize;
        }

        #endregion
    }
}
