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
    public void set_id(int id)
    {
        this.id = id;
    }

    public void set_user_id(int userID)
    {
        this.userID = userID;
    }

    public void set_data(BinaryData new_data)
    {
        data = new_data;
    }

    // getters
    public int get_id()
    {
        return id;
    }

    public int get_user_id()
    {
        return userID;
    }

    public BinaryData get_data()
    {
        return data;
    }

    public int calculate_size()
    {
        return data.Length;
    }
}