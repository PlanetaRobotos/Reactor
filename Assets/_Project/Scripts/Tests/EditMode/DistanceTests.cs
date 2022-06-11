using NUnit.Framework;
using submodules.CommonScripts.CommonScripts.Utilities.Tools;
using UnityEngine;

public class DistanceTests
{
    [Test]
    public void DistanceBetweenPoints()
    {
        const float minDistance = 300;

        Vector2 startPoint1 = new Vector2(0, 0);
        Vector2 startPoint2 = new Vector2(300, 300);
        bool actual = DistanceTools.IsAbove(minDistance, startPoint1, startPoint2);
        
        Assert.IsTrue(actual);
    }
    
    [Test]
    public void DistanceBetweenRandomPoints()
    {
        const float minDistance = 300;


        for (int i = 0; i < 100; i++)
        {
            Vector2[] points = DistanceTools.GetPoints(minDistance, 4);
            Vector2 startPoint1 = points[0];
            Vector2 startPoint2 = points[1];
        
            bool actual = DistanceTools.IsAbove(minDistance, startPoint1, startPoint2);
        
            Assert.IsTrue(actual);
        }
    }
}
