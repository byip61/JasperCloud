using System;

namespace JasperCloud.Models;

public class File
{
    private int id;
    private int userID;

    private BinaryData? data;
    public FileMetadata metadata;

    public File(BinaryData _data, FileMetadata _metadata)
    {
        data = _data;
        metadata = _metadata;
    }

    // setters
    public void SetId(int id)
    {
        this.id = id;
    }

    public void SetUserId(int userID)
    {
        this.userID = userID;
    }

    public void SetData(BinaryData new_data)
    {
        data = new_data;
    }

    // getters
    public int GetId()
    {
        return id;
    }

    public int GetUserId()
    {
        return userID;
    }

    public BinaryData GetData()
    {
        return data;
    }

    public int CalculateSize()
    {
        return data.Length;
    }
}