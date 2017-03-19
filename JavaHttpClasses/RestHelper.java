//import android.util.Base64;
import java.util.Base64;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;
import com.google.gson.JsonParseException;
import com.google.gson.JsonPrimitive;
import com.google.gson.JsonSerializationContext;
import com.google.gson.JsonSerializer;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.URL;

import java.lang.reflect.Type;

public class RestHelper
{
	public <TRequest, TResponse> TResponse ProcessRequest(TRequest request, Class<TResponse> respClass, String url)
	{
		TResponse response = null;
		
		try
		{
			Gson gson = new GsonBuilder()
				.setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
				.registerTypeHierarchyAdapter(byte[].class, new ByteArrayToBase64TypeAdapter())
				.create();
		
			String data = gson.toJson(request);
		
			URL urlObject = new URL(url);
			
			HttpURLConnection connection = (HttpURLConnection) urlObject.openConnection();
			
			connection.setDoInput(true);
			connection.setRequestProperty("Content-Type", "application/json");
			connection.setConnectTimeout(120000);
			connection.setReadTimeout(120000);
			connection.setRequestMethod("POST");
			connection.setDoOutput(true);
			
			OutputStreamWriter wr = new OutputStreamWriter(connection.getOutputStream());
			
			wr.write(data);
			wr.flush();
			wr.close();
			
			if(connection.getResponseCode() == HttpURLConnection.HTTP_OK)
			{
				InputStream inputStream = connection.getInputStream();
				
				BufferedReader reader = null;
				String inputLine;
				StringBuilder builder = new StringBuilder();
				
				reader = new BufferedReader(new InputStreamReader(inputStream,"utf-8"));
				
				while ((inputLine = reader.readLine()) != null)
				{
					builder.append(inputLine);
				}

				if (reader != null)
				{
					reader.close();
				}
				
				response = gson.fromJson(builder.toString(), respClass);
			}
			else
			{
				// Error handling?
				// System.out.println("An error occurred while sending the request: " + connection.getResponseMessage());
			}
		}
		catch (Exception ex)
		{
			// Error handling?
			ex.printStackTrace();
		}
		
		return response;
	}
	
    private static class ByteArrayToBase64TypeAdapter implements JsonSerializer<byte[]>, JsonDeserializer<byte[]>
	{
        public byte[] deserialize(JsonElement json, Type typeOfT, JsonDeserializationContext context) throws JsonParseException
		{
			return Base64.getDecoder().decode(json.getAsString());
            //return Base64.decode(json.getAsString(), Base64.NO_WRAP);
        }

        public JsonElement serialize(byte[] src, Type typeOfSrc, JsonSerializationContext context)
		{
			return new JsonPrimitive(Base64.getEncoder().encodeToString(src));
            //return new JsonPrimitive(Base64.encodeToString(src, Base64.NO_WRAP));
        }
    }
}
