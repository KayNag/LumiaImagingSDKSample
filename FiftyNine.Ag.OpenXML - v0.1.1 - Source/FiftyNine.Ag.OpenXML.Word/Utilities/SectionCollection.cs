using System;
using System.Net;
using System.Windows;
using System.Collections.ObjectModel;
using FiftyNine.Ag.OpenXML.Word.Elements;

namespace FiftyNine.Ag.OpenXML.Word.Utilities
{
    public class SectionCollection : Collection<Section>
    {
        protected override void InsertItem(int index, Section item)
        {
            if (index > 0)
            {
                item.PreviousSection = Items[index - 1];
            }
            if (index < Items.Count - 1)
            {
                Items[index].PreviousSection = item;
            }
            base.InsertItem(index, item);
        }
        protected override void RemoveItem(int index)
        {
            Section section = Items[index];
            section.PreviousSection = null;
            if (index == 0 && Items.Count > 1)
            {
                Items[1].PreviousSection = null;
            }
            else if (index > 0 && index < Items.Count - 1)
            {
                Items[index + 1].PreviousSection = Items[index - 1];
            }
            base.RemoveItem(index);
        }
        protected override void SetItem(int index, Section item)
        {
            if (index == 0)
            {
                item.PreviousSection = null;
            }
            else if (index > 0)
            {
                item.PreviousSection = Items[index - 1];
            }
            if (index < Items.Count - 1)
            {
                Items[index + 1].PreviousSection = item;
            }
            Items[index].PreviousSection = null;
            base.SetItem(index, item);
        }
    }
}
