﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ched.Components;
using Ched.UI;

namespace Ched.UI.Operations
{
    internal abstract class EditShortNoteOperation : IOperation
    {
        protected TappableBase Note { get; }
        public abstract string Description { get; }

        public EditShortNoteOperation(TappableBase note)
        {
            Note = note;
        }

        public abstract void Redo();
        public abstract void Undo();
    }

    internal class MoveShortNoteOperation : EditShortNoteOperation
    {
        public override string Description { get { return "ノートを移動"; } }

        protected NotePosition BeforePosition { get; }
        protected NotePosition AfterPosition { get; }

        public MoveShortNoteOperation(TappableBase note, NotePosition before, NotePosition after) : base(note)
        {
            BeforePosition = before;
            AfterPosition = after;
        }

        public override void Redo()
        {
            Note.Tick = AfterPosition.Tick;
            Note.LaneIndex = AfterPosition.Tick;
        }

        public override void Undo()
        {
            Note.Tick = BeforePosition.Tick;
            Note.LaneIndex = BeforePosition.LaneIndex;
        }

        public class NotePosition
        {
            public int Tick { get; }
            public int LaneIndex { get; }

            public NotePosition(int tick, int laneIndex)
            {
                Tick = tick;
                LaneIndex = laneIndex;
            }
        }
    }

    internal class ChangeShortNoteWidthOperation : EditShortNoteOperation
    {
        public override string Description { get { return "ノート幅の変更"; } }

        protected NotePosition BeforePosition { get; }
        protected NotePosition AfterPosition { get; }

        public ChangeShortNoteWidthOperation(TappableBase note, NotePosition before, NotePosition after) : base(note)
        {
            BeforePosition = before;
            AfterPosition = after;
        }

        public override void Redo()
        {
            Note.LaneIndex = AfterPosition.LaneIndex;
            Note.Width = AfterPosition.Width;
        }

        public override void Undo()
        {
            Note.LaneIndex = BeforePosition.LaneIndex;
            Note.Width = BeforePosition.Width;
        }

        public class NotePosition
        {
            public int LaneIndex { get; set; }
            public int Width { get; }

            public NotePosition(int laneIndex, int width)
            {
                LaneIndex = laneIndex;
                Width = width;
            }
        }
    }

    internal abstract class ManageShortNoteOperation<T> : IOperation
    {
        protected T Note { get; }
        protected NoteView.NoteCollection Collection { get; }
        public abstract string Description { get; }

        public ManageShortNoteOperation(T note)
        {
            Note = note;
        }

        public abstract void Undo();
        public abstract void Redo();
    }

    internal class InsertTapOperation : ManageShortNoteOperation<Tap>
    {
        public override string Description { get { return "TAPの追加"; } }

        public InsertTapOperation(Tap note) : base(note)
        {
        }

        public override void Redo()
        {
            Collection.Add(Note);
        }

        public override void Undo()
        {
            Collection.Remove(Note);
        }
    }

    internal class RemoveShortNoteOperation : ManageShortNoteOperation<Tap>
    {
        public override string Description { get { return "TAPの削除"; } }

        public RemoveShortNoteOperation(Tap note) : base(note)
        {
        }

        public override void Redo()
        {
            Collection.Add(Note);
        }

        public override void Undo()
        {
            Collection.Remove(Note);
        }
    }
}
