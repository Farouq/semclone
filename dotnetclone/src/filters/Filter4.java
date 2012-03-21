package filters;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.PrintWriter;

public class Filter4 {
	
	/**
	 * @param args
	 * Filtering br.s, brtrue.s,..... 
	 * 
	 */

	public static void filter(String fileName) throws Exception {

		String newFileName = fileName.subSequence(0, fileName.lastIndexOf('.'))+ "_f4.xml";
		File newf = new File(newFileName);
		if (newf.exists())
			newf.delete();

		BufferedReader in = new BufferedReader(new FileReader(fileName));
		PrintWriter out = new PrintWriter(new FileWriter(newf));

		String lin;
	

		while ((lin = in.readLine()) != null)
		{

			if (lin.trim().startsWith("br.s")||lin.trim().startsWith("brtrue.s")||
					lin.trim().startsWith("leave.s")||lin.trim().startsWith("blt.s")
					||lin.trim().startsWith("bge.s")||lin.trim().startsWith("ldloca.s")
					||lin.trim().startsWith("ldarga.s")||lin.trim().startsWith("ldc.i4.s"))
			{
				String[] temp=lin.trim().split(" ");
				lin = temp[0];
			}
			

			out.println(lin);
		//	System.out.println(lin);
		}

		in.close();
		out.close();
		System.out.println("Filter4 done");
	}

}
