import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.InputStreamReader;
import java.util.ArrayList;


public class Main {

	/**
	 * @param args
	 * @throws Exception 
	 */
	public static void main(String[] args) throws Exception {
		// TODO Auto-generated method stub
		runCommandWithOutput("your command", "C:\Users\faa634\Documents\Visual Studio 2010\Projects\ConsoleApplication1\ConsoleApplication1\bin\Debug\ConsoleApplication1.exe");
	}
	
	private static void runCommandWithOutput(String command, String outputFile) throws Exception
	{
		ArrayList<String> output=new ArrayList<String>();
		 Runtime rt = Runtime.getRuntime();
         Process pr = rt.exec(command);

         BufferedReader input = new BufferedReader(new InputStreamReader(pr.getInputStream()));

         String line=null;

         while((line=input.readLine()) != null) {
            // System.out.println(line);
             output.add(line);
         }

         int exitVal = pr.waitFor();
        /* if(exitVal!=0)
         {
         System.out.println("Exited with error code "+exitVal +"    command: "+command);
         }*/
         writeToFile(output, outputFile);
	}
	
	private static void writeToFile(ArrayList<String> lines,String fileAddress) throws Exception{
		  BufferedWriter bufferedWriter = null;
			bufferedWriter = new BufferedWriter(new FileWriter(fileAddress));
		 
			for(String s : lines)
			{
				
		 
			    bufferedWriter.write(s);
			  bufferedWriter.newLine();
			}
			 
			  bufferedWriter.flush();
			  bufferedWriter.close();  
	}

}
