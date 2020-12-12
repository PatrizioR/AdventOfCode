using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Shared
{
    public static class SeatSystemHelper
    {
        public static int CountSeatsOnNoMoreChanges(string input, bool limit = true, int minOccupiedNumber = 4)
        {
            SeatType[][] seatLines = GetSeats(input);

            var count = 0;

            do
            {
                count = CountOccupied(seatLines);
                ProcessRound(seatLines, limit, minOccupiedNumber);
            } while (count != CountOccupied(seatLines));

            return count;
        }

        public static SeatType[][] GetSeats(string input)
        {
            var map = input.TrimmedNotEmptyMap();
            var seatLines = map.Select(item => MapSeatType(item)).Select(a => a.ToArray()).ToArray();
            return seatLines;
        }

        private static string SeatsToString(SeatType[][] seatLines)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var seatLine in seatLines)
            {
                foreach (var seat in seatLine)
                {
                    sb.Append(seat == SeatType.Floor ? "." : seat == SeatType.Free ? "L" : "#");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private static SeatType[][] Copy(SeatType[][] seatLines)
        {
            var result = new SeatType[seatLines.Length][];
            for (int i = 0; i < seatLines.Length; i++)
            {
                result[i] = new SeatType[seatLines[i].Length];
                for (var j = 0; j < seatLines[i].Length; j++)
                {
                    result[i][j] = seatLines[i][j];
                }
            }
            return result;
        }

        private static void ProcessRound(SeatType[][] seatLines, bool limit, int minOccupiedNumber)
        {
            var copy = Copy(seatLines);
            for (int i = 0; i < copy.Count(); i++)
            {
                ProcessLine(copy, seatLines, i, limit, minOccupiedNumber);
            }
        }

        public static List<SeatType> GetAdjacents(SeatType[][] seatLines, int line, int row, bool limit = true, SeatType search = SeatType.Occupied)
        {
            List<SeatType> result = new List<SeatType>();

            // to the right
            for (int i = 1; i < seatLines[line].Length - row; i++)
            {
                SeatType seatType = seatLines[line][row + i];
                if (seatType != SeatType.Floor)
                {
                    if (seatType == search)
                    {
                        result.Add(seatType);
                    }
                    break;
                }

                if (limit)
                {
                    break;
                }
            }

            // to the down
            for (int i = 1; i < seatLines.Length - line; i++)
            {
                SeatType seatType = seatLines[line + i][row];
                if (seatType != SeatType.Floor)
                {
                    if (seatType == search)
                    {
                        result.Add(seatType);
                    }
                    break;
                }
                if (limit)
                {
                    break;
                }
            }

            // to the up
            for (int i = 1; i <= line; i++)
            {
                SeatType seatType = seatLines[line - i][row];
                if (seatType != SeatType.Floor)
                {
                    if (seatType == search)
                    {
                        result.Add(seatType);
                    }
                    break;
                }
                if (limit)
                {
                    break;
                }
            }

            // to the left
            for (int i = 1; i <= row; i++)
            {
                SeatType seatType = seatLines[line][row - i];
                if (seatType != SeatType.Floor)
                {
                    if (seatType == search)
                    {
                        result.Add(seatType);
                    }
                    break;
                }
                if (limit)
                {
                    break;
                }
            }

            // to the right down
            for (int i = 1; i < seatLines[line].Length - row; i++)
            {
                if (line + i >= seatLines.Length)
                {
                    break;
                }

                SeatType seatType = seatLines[line + i][row + i];
                if (seatType != SeatType.Floor)
                {
                    if (seatType == search)
                    {
                        result.Add(seatType);
                    }
                    break;
                }
                if (limit)
                {
                    break;
                }
            }

            // to the left down
            for (int i = 1; i <= row; i++)
            {
                if (line + i >= seatLines.Length)
                {
                    break;
                }

                SeatType seatType = seatLines[line + i][row - i];
                if (seatType != SeatType.Floor)
                {
                    if (seatType == search)
                    {
                        result.Add(seatType);
                    }
                    break;
                }
                if (limit)
                {
                    break;
                }
            }

            // to the left up
            for (int i = 1; i <= row; i++)
            {
                if (line - i < 0)
                {
                    break;
                }

                SeatType seatType = seatLines[line - i][row - i];
                if (seatType != SeatType.Floor)
                {
                    if (seatType == search)
                    {
                        result.Add(seatType);
                    }
                    break;
                }
                if (limit)
                {
                    break;
                }
            }

            // to the right up
            for (int i = 1; i <= line; i++)
            {
                if (row + i >= seatLines[line].Length)
                {
                    break;
                }

                SeatType seatType = seatLines[line - i][row + i];
                if (seatType != SeatType.Floor)
                {
                    if (seatType == search)
                    {
                        result.Add(seatType);
                    }
                    break;
                }
                if (limit)
                {
                    break;
                }
            }

            Debug.Assert(result.Count() <= 8, $"Too many seats adjacent {result.Count()}, maximum 8");
            return result;
        }

        private static void ProcessLine(SeatType[][] orgSeatLines, SeatType[][] seatLines, int line, bool limit, int minOccupiedNumber)
        {
            for (int i = 0; i < orgSeatLines[line].Count(); i++)
            {
                if (orgSeatLines[line][i] == SeatType.Floor)
                {
                    // floor ignored
                    continue;
                }

                if (orgSeatLines[line][i] == SeatType.Free)
                {
                    // if a seat is empty and there are no occupied seats adjecent to it occupy
                    var canOccupy = !GetAdjacents(orgSeatLines, line, i, limit, SeatType.Occupied).Any();
                    if (canOccupy)
                    {
                        seatLines[line][i] = SeatType.Occupied;
                    }
                }
                else if (orgSeatLines[line][i] == SeatType.Occupied)
                {
                    // if a seat is occupied and four or more seats adjecent to it are also occupied, empty it
                    var count = GetAdjacents(orgSeatLines, line, i, limit, SeatType.Occupied).Count();
                    if (count >= minOccupiedNumber)
                    {
                        seatLines[line][i] = SeatType.Free;
                    }
                }
            }
        }

        private static int CountOccupied(SeatType[][] seatLines)
        {
            return seatLines.SelectMany(item => item.Where(curItem => curItem == SeatType.Occupied)).Count();
        }

        public enum SeatType
        {
            Free,
            Occupied,
            Floor
        }

        public static List<SeatType> MapSeatType(string line)
        {
            var result = new List<SeatType>();
            foreach (var point in line)
            {
                switch (point)
                {
                    case 'L':
                        result.Add(SeatType.Free);
                        break;

                    case '.':
                        result.Add(SeatType.Floor);
                        break;

                    case '#':
                        result.Add(SeatType.Occupied);
                        break;

                    default:
                        throw new ArgumentException($"Invalid char: {point}", nameof(point));
                }
            }
            return result;
        }
    }
}