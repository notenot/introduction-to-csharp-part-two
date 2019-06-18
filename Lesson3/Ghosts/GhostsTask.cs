using System;
using System.Text;

namespace hashes
{
	public class GhostsTask : 
		IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, 
		IMagic
    {
        private readonly Document linkToDocument;
        private readonly Vector linkToVector;
        private readonly Segment linkToSegment;
        private readonly Cat linkToCat;
        private readonly Robot linkToRobot;

        private readonly byte[] documentContent;

        public GhostsTask()
        {
            documentContent = Encoding.UTF8.GetBytes("simple content");
            linkToDocument = new Document(
                "simple title", 
                Encoding.UTF8,
                documentContent);
            linkToVector = new Vector(0.0, 0.0);
            linkToSegment = new Segment(linkToVector, linkToVector);
            linkToCat = new Cat(
                "cat name", 
                "cat breed", 
                new DateTime(1995, 7, 8, 13, 13, 13));
            linkToRobot = new Robot("simple id", 100.0);
        }

		public void DoMagic()
        {
            unchecked
            {
                for (var i = 0; i < documentContent.Length; ++i)
                    ++documentContent[i];
            }
            linkToVector.Add(new Vector(1.0, 1.0));
            linkToCat.Rename("another cat name");
            Robot.BatteryCapacity /= 10;
        }

        Vector IFactory<Vector>.Create() =>
            linkToVector;

        Segment IFactory<Segment>.Create() =>
            linkToSegment;

        Document IFactory<Document>.Create() =>
            linkToDocument;

        Cat IFactory<Cat>.Create() =>
            linkToCat;

        Robot IFactory<Robot>.Create() =>
            linkToRobot;
    }
}