﻿using System;
using System.Collections.Generic;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Rendering.Console;
using NetAF.Rendering.FrameBuilders;

namespace SSHammerhead.Assets.Players.SpiderBot.FrameBuilders
{
    /// <summary>
    /// Provides a console room map builder.
    /// </summary>
    /// <param name="gridStringBuilder">The string builder to use.</param>
    public sealed class BotConsoleRoomMapBuilder(GridStringBuilder gridStringBuilder) : IRoomMapBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the character used for representing a locked exit.
        /// </summary>
        public char LockedExit { get; set; } = 'x';

        /// <summary>
        /// Get or set the character used for representing there is an item or a character in the room.
        /// </summary>
        public char ItemOrCharacterInRoom { get; set; } = '?';

        /// <summary>
        /// Get or set the character to use for vertical boundaries.
        /// </summary>
        public char VerticalBoundary { get; set; } = '.';

        /// <summary>
        /// Get or set the character to use for horizontal boundaries.
        /// </summary>
        public char HorizontalBoundary { get; set; } = '.';

        /// <summary>
        /// Get or set the character to use for vertical exit borders.
        /// </summary>
        public char VerticalExitBorder { get; set; } = '.';

        /// <summary>
        /// Get or set the character to use for horizontal exit borders.
        /// </summary>
        public char HorizontalExitBorder { get; set; } = '.';

        /// <summary>
        /// Get or set the character to use for corners.
        /// </summary>
        public char Corner { get; set; } = '.';

        /// <summary>
        /// Get or set the padding between the key and the map.
        /// </summary>
        public int KeyPadding { get; set; } = 2;

        /// <summary>
        /// Get or set the room boundary color.
        /// </summary>
        public AnsiColor BoundaryColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the item or character color.
        /// </summary>
        public AnsiColor ItemOrCharacterColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the locked exit color.
        /// </summary>
        public AnsiColor LockedExitColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the visited exit color.
        /// </summary>
        public AnsiColor VisitedExitColor { get; set; } = SpiderBotTemplate.DisplayColor;

        /// <summary>
        /// Get or set the unvisited exit color.
        /// </summary>
        public AnsiColor UnvisitedExitColor { get; set; } = SpiderBotTemplate.DisplayColor;

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
            gridStringBuilder.SetCell(startPosition.X, startPosition.Y, Corner, BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 1, startPosition.Y, HorizontalBoundary, BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y, VerticalExitBorder, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y, VerticalExitBorder, BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y, VerticalExitBorder, BoundaryColor);

                if (viewPoint[Direction.North]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, 'n', VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, 'N', UnvisitedExitColor);

                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y, VerticalExitBorder, BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 3, startPosition.Y, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 5, startPosition.Y, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y, HorizontalBoundary, BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X + 7, startPosition.Y, HorizontalBoundary, BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y, Corner, BoundaryColor);
        }

        /// <summary>
        /// Draw the south border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawSouthBorder(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 6, Corner, BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 1, startPosition.Y + 6, HorizontalBoundary, BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 6, VerticalExitBorder, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 6, VerticalExitBorder, BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 6, VerticalExitBorder, BoundaryColor);

                if (viewPoint[Direction.South]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, 's', VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, 'S', UnvisitedExitColor);

                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 6, VerticalExitBorder, BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 6, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 3, startPosition.Y + 6, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 6, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 5, startPosition.Y + 6, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 6, HorizontalBoundary, BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X + 7, startPosition.Y + 6, HorizontalBoundary, BoundaryColor);
            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 6, Corner, BoundaryColor);
        }

        /// <summary>
        /// Draw the east border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawEastBorder(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 1, VerticalBoundary, BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.East))
            {
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 2, HorizontalExitBorder, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 4, HorizontalExitBorder, BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.East))
            {
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 2, HorizontalExitBorder, BoundaryColor);

                if (viewPoint[Direction.East]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, 'e', VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, 'E', UnvisitedExitColor);

                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 4, HorizontalExitBorder, BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 2, VerticalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 3, VerticalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 4, VerticalBoundary, BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X + 8, startPosition.Y + 5, VerticalExitBorder, BoundaryColor);
        }

        /// <summary>
        /// Draw the west border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="startPosition">The start position.</param>
        private void DrawWestBorder(Room room, ViewPoint viewPoint, Point2D startPosition)
        {
            gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 1, VerticalBoundary, BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.West))
            {
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 2, HorizontalExitBorder, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 4, HorizontalExitBorder, BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.West))
            {
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 2, HorizontalExitBorder, BoundaryColor);

                if (viewPoint[Direction.West]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, 'w', VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, 'W', UnvisitedExitColor);

                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 4, HorizontalExitBorder, BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 2, VerticalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 3, VerticalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 4, VerticalBoundary, BoundaryColor);
            }

            gridStringBuilder.SetCell(startPosition.X, startPosition.Y + 5, VerticalExitBorder, BoundaryColor);
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
                gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 2, LockedExit, LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.Up))
            {
                if (viewPoint[Direction.Up]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 2, 'u', VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startPosition.X + 2, startPosition.Y + 2, 'U', UnvisitedExitColor);
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
                gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 2, LockedExit, LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.Down))
            {
                if (viewPoint[Direction.Down]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 2, 'd', VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startPosition.X + 6, startPosition.Y + 2, 'D', UnvisitedExitColor);
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
                gridStringBuilder.SetCell(startPosition.X + 4, startPosition.Y + 3, ItemOrCharacterInRoom, ItemOrCharacterColor);
        }

        /// <summary>
        /// Draw the key.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="key">The key type.</param>
        /// <param name="startPosition">The start position.</param>
        /// <param name="endX">The end position, x.</param>
        /// <param name="endY">The end position, x.</param>
        private void DrawKey(Room room, ViewPoint viewPoint, KeyType key, Point2D startPosition, out int endX, out int endY)
        {
            var keyLines = new Dictionary<string, AnsiColor>();
            var lockedExitString = $"{LockedExit} = Locked Exit";
            var notVisitedExitString = "N/E/S/W/U/D = Unvisited";
            var visitedExitString = "n/e/s/w/u/d = Visited";
            var itemsString = $"{ItemOrCharacterInRoom} = Item(s) or Character(s) in Room";

            switch (key)
            {
                case KeyType.Dynamic:

                    if (room.Exits.Where(x => x.IsPlayerVisible).Any(x => x.IsLocked))
                        keyLines.Add(lockedExitString, LockedExitColor);

                    if (viewPoint.AnyNotVisited)
                        keyLines.Add(notVisitedExitString, UnvisitedExitColor);

                    if (viewPoint.AnyVisited)
                        keyLines.Add(visitedExitString, VisitedExitColor);

                    if (room.EnteredFrom.HasValue)
                        keyLines.Add($"{room.EnteredFrom.Value.ToString().ToLower()[..1]} = Entrance", VisitedExitColor);

                    if (Array.Exists(room.Items, x => x.IsPlayerVisible) || Array.Exists(room.Characters, x => x.IsPlayerVisible))
                        keyLines.Add(itemsString, ItemOrCharacterColor);

                    break;

                case KeyType.Full:

                    keyLines.Add(lockedExitString, LockedExitColor);
                    keyLines.Add(notVisitedExitString, UnvisitedExitColor);
                    keyLines.Add(visitedExitString, VisitedExitColor);
                    keyLines.Add(itemsString, ItemOrCharacterColor);

                    break;

                case KeyType.None:
                    break;
                default:
                    throw new NotImplementedException();
            }

            endX = startPosition.X;
            endY = startPosition.Y + 7;

            if (keyLines.Count == 0)
                return;

            var maxCommandLength = keyLines.Max(x => x.Key.Length);
            var maxWidth = maxCommandLength + endX + 1;
            endY += KeyPadding;
            var startKeyX = Math.Max(endX + 4 - maxCommandLength / 2, 0);

            foreach (var keyLine in keyLines)
                gridStringBuilder.DrawWrapped(keyLine.Key, startKeyX, endY + 1, maxWidth, keyLine.Value, out endX, out endY);
        }

        #endregion

        #region Implementation of IRoomMapBuilder

        /// <summary>
        /// Build a map for a room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="key">The key type.</param>
        /// <param name="startPosition">The start position.</param>
        /// <param name="endX">The end position, x.</param>
        /// <param name="endY">The end position, x.</param>
        public void BuildRoomMap(Room room, ViewPoint viewPoint, KeyType key, Point2D startPosition, out int endX, out int endY)
        {
            DrawNorthBorder(room, viewPoint, startPosition);
            DrawSouthBorder(room, viewPoint, startPosition);
            DrawEastBorder(room, viewPoint, startPosition);
            DrawWestBorder(room, viewPoint, startPosition);
            DrawUpExit(room, viewPoint, startPosition);
            DrawDownExit(room, viewPoint, startPosition);
            DrawItemOrCharacter(room, startPosition);
            DrawKey(room, viewPoint, key, startPosition, out endX, out endY);

            if (endY < startPosition.Y + 6)
                endY = startPosition.Y + 6;
        }

        #endregion
    }
}
