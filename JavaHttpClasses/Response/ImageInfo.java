import java.util.Date;

public class ImageInfo
{
	private String FileName;
	private String FileFormat;
	private byte[] FileData;
	private Date DateLastChanged;
	
	public String getFileName()
	{
		return FileName;
	}
	
	public String getFileFormat()
	{
		return FileFormat;
	}
	
	public byte[] getFileData()
	{
		return FileData;
	}
	
	public Date getDataLastChanged()
	{
		return DateLastChanged;
	}
	
	public void setFileName(String FileName)
	{
		this.FileName = FileName;
	}
	
	public void setFileFormat(String FileFormat)
	{
		this.FileFormat = FileFormat;
	}
	
	public void setFileData(byte[] FileData)
	{
		this.FileData = FileData;
	}
	
	public void setDateLastChanged(Date DateLastChanged)
	{
		this.DateLastChanged = DateLastChanged;
	}
}