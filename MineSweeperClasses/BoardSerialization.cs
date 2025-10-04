using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MineSweeperClasses
{
    // What the save file looks like (simple, stable).
    public class BoardSave
    {
        public int Size { get; set; }
        public float Difficulty { get; set; }
        public DateTime StartTime { get; set; }
        public int RewardsRemaining { get; set; }
        public Board.GameStatus GameState { get; set; }
        public CellSave[][] Cells { get; set; } = Array.Empty<CellSave[]>();
    }

    public class CellSave
    {
        public bool Live { get; set; }
        public int LiveNeighbors { get; set; }
        public bool HasReward { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
    }

    public static class BoardSerialization
    {
        private static readonly JsonSerializerOptions _json =
            new JsonSerializerOptions { WriteIndented = true };

        // Convert a live Board object to a serializable snapshot.
        public static BoardSave ToSave(Board b)
        {
            var save = new BoardSave
            {
                Size = b.Size,
                Difficulty = b.Difficulty,
                StartTime = b.StartTime,
                RewardsRemaining = b.RewardsRemaining,
                GameState = b.DetermineGameState(),
                Cells = new CellSave[b.Size][]
            };

            for (int r = 0; r < b.Size; r++)
            {
                save.Cells[r] = new CellSave[b.Size];
                for (int c = 0; c < b.Size; c++)
                {
                    var cell = b.Cells[r, c];
                    save.Cells[r][c] = new CellSave
                    {
                        Live = cell.Live,
                        LiveNeighbors = cell.LiveNeighbors,
                        HasReward = cell.HasReward,
                        IsRevealed = cell.IsRevealed,
                        IsFlagged = cell.IsFlagged
                    };
                }
            }

            return save;
        }

        // Rebuild a playable Board from a snapshot.
        public static Board FromSave(BoardSave s)
        {
            // Create a fresh board (constructor wires everything),
            // then overwrite state from the snapshot.
            var b = new Board(s.Size, s.Difficulty)
            {
                StartTime = s.StartTime,
                RewardsRemaining = s.RewardsRemaining
            };

            for (int r = 0; r < s.Size; r++)
            {
                for (int c = 0; c < s.Size; c++)
                {
                    var src = s.Cells[r][c];
                    var dst = b.Cells[r, c];

                    // Set the full state without triggering gameplay side effects.
                    dst.SetState(
                        live: src.Live,
                        liveNeighbors: src.LiveNeighbors,
                        hasReward: src.HasReward,
                        isRevealed: src.IsRevealed,
                        isFlagged: src.IsFlagged
                    );
                }
            }

            return b;
        }

        // Serialize to JSON string.
        public static string ToJson(Board b) =>
            JsonSerializer.Serialize(ToSave(b), _json);

        // Deserialize from JSON string.
        public static Board FromJson(string json)
        {
            var save = JsonSerializer.Deserialize<BoardSave>(json)
                       ?? throw new InvalidOperationException("Invalid save file.");
            return FromSave(save);
        }
    }
}

