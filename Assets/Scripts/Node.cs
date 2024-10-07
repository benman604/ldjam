using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NodeBase
{
    public NodeBase Connection { get; private set; }
    public float G { get; private set; }
    public float H { get; private set; }
    public float F => G + H;

    public void SetConnection(NodeBase nodebase) { Connection = nodebase; }
    public void SetG(float g) { G = g; }
    public void SetH(float h) { H = h; }
}

public static list<NodeBase> FindPath(NodeBase startNode, NodeBase targetNode)
{
    var toSearch = new List<NodeBase>() { startNode };
    var processed = new List<NodeBase>();
    while (toSearch.Any())
    {
        var current = toSearch[0];
        foreach (var t in toSearch)
        {
            if (t.F < current.F || t.F == current.F && t.H < current.H)
            {
                current = t;
            }
        }
        processed.Add(current);
        toSearch.Remove(current);

        if(current == targetNode)
        {
            var currentPathTile = targetNode;
            var path = new List<NodeBase>();
            while(currentPathTile != startNode)
            {
                path.Add(currentPathTile);
                currentPathTile = currentPathTile.Connection;
            }

            return path;
        }


        foreach (var neighbor in current.Neighbors.Where(t => t.Walkable && !processed.Contains(t)))
        {
            var inSearch = toSearch.Contains(neighbor);

            var costToNeighbor = current.G + current.GetDistance(neighbor);

            if (!inSearch || costToNeighbor < neighbor.G)
            {
                neighbor.SetG(costToNeighbor);
                neighbor.SetConnection(current);

                if (!inSearch)
                {
                    neighbor.SetH(neighbor.GetDistance(targetNode));
                    toSearch.Add(neighbor);
                }
            }
        }
    }
}