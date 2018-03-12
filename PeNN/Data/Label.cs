using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeNN.Data
{
    public class Label
    {
        public string textLabel;
        public int clusterIndex;

        public Info data;

        public Label()
        {
            this.textLabel = null;
            this.clusterIndex = -1;
            this.data = null;
        }
        
        public Label(string label)
        {
            this.textLabel = label;
        }

        public Label(int cluster)
        {
            this.clusterIndex = cluster;
        }

        public Label(Info data)
        {
            this.data = data;
        }

        internal Label Clone()
        {
            var label = new Label();

            label.clusterIndex = this.clusterIndex;
            label.data = this.data;

            var serialized = JsonConvert.SerializeObject(this.data);
            label.data = JsonConvert.DeserializeObject<Info>(serialized);

            return label;
        }
    }
}
