using System;
using System.Collections.Generic;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;

namespace SSHammerhead.Assets.Players.SpiderBot.FrameBuilders.Console
{
    /// <summary>
    /// Provides a console builder for region maps.
    /// </summary>
    /// <param name="gridStringBuilder">The string builder to use.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class BotConsoleRegionMapBuilder(GridStringBuilder gridStringBuilder, ConsoleMapRenderProperties properties) : IConsoleRegionMapBuilder
    {
        #region Methods

        /// <summary>
        /// Draw a room on the current floor.
        /// </summary>
        /// <param name="room">The room to draw.</param>
        /// <param name="topLeft">The top left of the room.</param>
        /// <param name="isPlayerRoom">True if this is the player room.</param>
        /// <param name="isFocusRoom">True if this is the focus room.</param>
        private void DrawCurrentFloorRoom(Room room, Point2D topLeft, bool isPlayerRoom, bool isFocusRoom)
        {
            /*
             * |   |
             *  ^Ov|
             * |---|
             */

            AnsiColor color = properties.UnvisitedBoundaryColor;

            if (isFocusRoom)
                color = properties.FocusedBoundaryColor;
            else if (room.HasBeenVisited)
                color = properties.VisitedBoundaryColor;

            gridStringBuilder.SetCell(topLeft.X, topLeft.Y, properties.VerticalBoundary, color);

            if (room.HasLockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y, properties.LockedExit, properties.LockedExitColor);
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y, properties.LockedExit, properties.LockedExitColor);
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y, properties.LockedExit, properties.LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y, properties.UnLockedExit, color);
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y, properties.UnLockedExit, color);
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y, properties.UnLockedExit, color);
            }
            else
            {
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y, properties.HorizontalBoundary, color);
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y, properties.HorizontalBoundary, color);
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y, properties.HorizontalBoundary, color);
            }

            gridStringBuilder.SetCell(topLeft.X + 4, topLeft.Y, properties.VerticalBoundary, color);

            if (room.HasLockedExitInDirection(Direction.West))
                gridStringBuilder.SetCell(topLeft.X, topLeft.Y + 1, properties.LockedExit, properties.LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.West))
                gridStringBuilder.SetCell(topLeft.X, topLeft.Y + 1, properties.UnLockedExit, color);
            else
                gridStringBuilder.SetCell(topLeft.X, topLeft.Y + 1, properties.VerticalBoundary, color);

            if (room.HasLockedExitInDirection(Direction.Up))
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y + 1, properties.LockedExit, properties.LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.Up))
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y + 1, '^', color);
            else
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y + 1, properties.EmptySpace, color);

            if (isPlayerRoom)
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y + 1, properties.Player, properties.PlayerColor);
            else if (isFocusRoom)
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y + 1, properties.Focus, properties.FocusedBoundaryColor);
            else
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y + 1, properties.EmptySpace, color);

            if (room.HasLockedExitInDirection(Direction.Down))
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y + 1, properties.LockedExit, properties.LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.Down))
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y + 1, 'v', color);
            else
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y + 1, properties.EmptySpace, color);

            if (room.HasLockedExitInDirection(Direction.East))
                gridStringBuilder.SetCell(topLeft.X + 4, topLeft.Y + 1, properties.LockedExit, properties.LockedExitColor);
            else if (room.HasUnlockedExitInDirection(Direction.East))
                gridStringBuilder.SetCell(topLeft.X + 4, topLeft.Y + 1, properties.UnLockedExit, color);
            else
                gridStringBuilder.SetCell(topLeft.X + 4, topLeft.Y + 1, properties.VerticalBoundary, color);

            gridStringBuilder.SetCell(topLeft.X, topLeft.Y + 2, properties.VerticalBoundary, color);

            if (room.HasLockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y + 2, properties.LockedExit, properties.LockedExitColor);
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y + 2, properties.LockedExit, properties.LockedExitColor);
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y + 2, properties.LockedExit, properties.LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y + 2, properties.UnLockedExit, color);
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y + 2, properties.UnLockedExit, color);
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y + 2, properties.UnLockedExit, color);
            }
            else
            {
                gridStringBuilder.SetCell(topLeft.X + 1, topLeft.Y + 2, properties.HorizontalBoundary, color);
                gridStringBuilder.SetCell(topLeft.X + 2, topLeft.Y + 2, properties.HorizontalBoundary, color);
                gridStringBuilder.SetCell(topLeft.X + 3, topLeft.Y + 2, properties.HorizontalBoundary, color);
            }

            gridStringBuilder.SetCell(topLeft.X + 4, topLeft.Y + 2, properties.VerticalBoundary, color);
        }

        /// <summary>
        /// Draw a room on a lower level.
        /// </summary>
        /// <param name="topLeft">The top left of the room.</param>
        private void DrawLowerLevelRoom(Point2D topLeft)
        {
            /*
             * .....
             * .....
             * .....
             *
             */

            for (var y = 0; y < 3; y++)
                for (var x = 0; x < 5; x++)
                    gridStringBuilder.SetCell(topLeft.X + x, topLeft.Y + y, properties.LowerLevel, properties.LowerLevelColor);

        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Try and convert a position in a matrix to a grid layout position.
        /// </summary>
        /// <param name="gridStartPosition">The position to start building at.</param>
        /// <param name="availableSize">The available size, in the grid.</param>
        /// <param name="matrix">The matrix.</param>
        /// <param name="roomPosition">The position of the room, in the matrix.</param>
        /// <param name="focusPosition">The focus position, in the matrix.</param>
        /// <param name="gridLeft">The left position to begin rendering the room at, in the grid.</param>
        /// <param name="gridTop">The top position to begin rendering the room at, in the grid.</param>
        /// <returns>True if the matrix position could be converted to a grid position and fit in the available space.</returns>
        private static bool TryConvertMatrixPositionToGridLayoutPosition(Point2D gridStartPosition, Size availableSize, Matrix matrix, Point2D roomPosition, Point2D focusPosition, out int gridLeft, out int gridTop)
        {
            const int roomWidth = 5;
            const int roomHeight = 3;

            // set position of room, Y is inverted
            gridLeft = gridStartPosition.X + roomPosition.X * roomWidth;
            gridTop = gridStartPosition.Y + (matrix.Height - 1) * roomHeight - roomPosition.Y * roomHeight;

            // check if map will fit
            if (matrix.Width * roomWidth > availableSize.Width || matrix.Height * roomHeight > availableSize.Height)
            {
                // centralise on focus position
                gridLeft += availableSize.Width / 2 - focusPosition.X * roomWidth + roomWidth / 2;
                gridTop += availableSize.Height / 2 + (focusPosition.Y - matrix.Height) * roomHeight - roomHeight / 2;
            }
            else
            {
                // centralise on area
                gridLeft += (int)Math.Floor(availableSize.Width / 2d - matrix.Width / 2d * roomWidth);
                gridTop += (int)Math.Floor(availableSize.Height / 2d - matrix.Height / 2d * roomHeight);
            }

            return gridLeft >= gridStartPosition.X &&
                   gridLeft + roomWidth - 1 < availableSize.Width &&
                   gridTop >= gridStartPosition.Y &&
                   gridTop + roomHeight - 1 < availableSize.Height;
        }

        #endregion

        #region Implementation of IRegionMapBuilder

        /// <inheritdoc/>
        public void BuildRegionMap(Region region, Point3D focusPosition, RegionMapRenderOptions regionMapOptions, RoomMapRenderOptions roomMapOptions, Size maxSize)
        {
            BuildRegionMap(region, focusPosition, regionMapOptions, roomMapOptions, maxSize, new(0, 0));
        }

        #endregion

        #region Implementation of IConsoleRegionMapBuilder

        /// <inheritdoc/>
        public void BuildRegionMap(Region region, Point3D focusPosition, RegionMapRenderOptions regionMapOptions, RoomMapRenderOptions roomMapOptions, Size maxSize, Point2D startPosition)
        {
            var matrix = region.ToMatrix();
            var playerRoom = region.GetPositionOfRoom(region.CurrentRoom);
            var playerFloor = playerRoom.Position.Z;
            var focusFloor = focusPosition.Z;
            var rooms = matrix.ToRooms().Where(r => r != null).ToArray();
            var unvisitedRoomPositions = rooms.Select(region.GetPositionOfRoom).Where(r => !r.Room.HasBeenVisited).ToList();
            var visitedRoomPositions = rooms.Select(region.GetPositionOfRoom).Where(r => r.Room.HasBeenVisited).ToList();
            var multiLevel = matrix.Depth > 1 && (region.IsVisibleWithoutDiscovery || matrix.FindAllZWithVisitedRooms().Length > 1);
            var indicatorLength = 3 + matrix.Depth.ToString().Length;
            var maxAvailableWidth = maxSize.Width;
            var x = startPosition.X;
            var y = startPosition.Y;

            if (multiLevel)
            {
                // draw floor indicators

                for (var floor = matrix.Depth - 1; floor >= 0; floor--)
                {
                    var roomsOnThisFloor = rooms.Where(r => region.GetPositionOfRoom(r).Position.Z == floor).ToArray();

                    // only draw levels indicators where a region is visible without discovery or a room on the floor has been visited
                    if (!region.IsVisibleWithoutDiscovery && !Array.Exists(roomsOnThisFloor, r => r.HasBeenVisited))
                        continue;

                    var isFocusFloor = floor == focusFloor;

                    if (floor == playerFloor)
                        gridStringBuilder.DrawWrapped($"{properties.CurrentFloorIndicator} L{floor}", x, ++y, maxAvailableWidth, properties.VisitedBoundaryColor, out _, out _);
                    else
                        gridStringBuilder.DrawWrapped($"L{floor}", x + 2, ++y, maxAvailableWidth, isFocusFloor ? properties.FocusedBoundaryColor : properties.LowerLevelColor, out _, out _);
                }

                x += indicatorLength;
                maxAvailableWidth -= indicatorLength;
            }

            // firstly draw lower levels
            if (properties.ShowLowerFloors)
            {
                List<RoomPosition> lowerLevelRooms = [.. visitedRoomPositions.Where(r => r.Position.Z < focusFloor)];

                if (region.IsVisibleWithoutDiscovery)
                    lowerLevelRooms.AddRange(unvisitedRoomPositions.Where(r => r.Position.Z < focusFloor));

                foreach (var position in lowerLevelRooms)
                {
                    if (TryConvertMatrixPositionToGridLayoutPosition(new Point2D(x, y), new Size(maxAvailableWidth, maxSize.Height), matrix, new Point2D(position.Position.X, position.Position.Y), new Point2D(focusPosition.X, focusPosition.Y), out var left, out var top))
                        DrawLowerLevelRoom(new Point2D(left, top));
                }
            }

            // now focus level
            List<RoomPosition> focusLevelRooms = [.. visitedRoomPositions.Where(r => r.Position.Z == focusFloor)];

            if (region.IsVisibleWithoutDiscovery)
                focusLevelRooms.AddRange(unvisitedRoomPositions.Where(r => r.Position.Z == focusFloor));

            foreach (var position in focusLevelRooms)
            {
                if (TryConvertMatrixPositionToGridLayoutPosition(new Point2D(x, y), new Size(maxAvailableWidth, maxSize.Height), matrix, new Point2D(position.Position.X, position.Position.Y), new Point2D(focusPosition.X, focusPosition.Y), out var left, out var top))
                    DrawCurrentFloorRoom(position.Room, new Point2D(left, top), position.Room == playerRoom.Room, position.Position.Equals(focusPosition));
            }
        }

        #endregion
    }
}

