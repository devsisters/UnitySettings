using Settings.GUI;
using UnityEngine;

public class SampleListView : ListView
{
    class ListViewDelegate : IListViewDelegate
    {
        public int Count => 11;

        public ListViewItem GetItem(int index)
        {
            var shouldHighlight = (index - 1) % 5 == 0;
            return new ListViewItem("List View Item: " + index, shouldHighlight);
        }

        public void OnSelect(int index)
        {
            Debug.Log("OnSelect: " + index);
        }
    }

    public SampleListView()
        : base("Sample List View", null, 80, new ListViewDelegate())
    {
    }
}