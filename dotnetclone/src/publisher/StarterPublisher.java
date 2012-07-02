package publisher;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;

import java.io.InputStreamReader;
import java.util.ArrayList;
import configuration.Configuration;




public class StarterPublisher {

	/**
	 * @param args
	 * @throws Exception 
	 */



	public static void main(String[] args) throws Exception {

	}

	public static void start(Configuration config) throws Exception
	{
		System.out.println("ver alpha 66");

		System.out.println("step1");
		ArrayList<String> files=	step1_findExeFiles(config);
		System.out.println("step2");
		ArrayList<ArrayList<String>> disassembledCodeList= step2_disAssemble(config,files);


		System.out.println("step3");
		step3_writeDisassembleContentToFiles(config,files,disassembledCodeList,false);

		System.out.println("step4");
		step4_exportToXML(config,files,disassembledCodeList);

		System.out.println("step5");
		step5_Filter_ExportAllToXML(config,files,disassembledCodeList);


		System.out.println("ver plusma");
		System.out.println("ver plusma DONE");
	}


	private static ArrayList<String> step1_findExeFiles(Configuration config)
	{
		Crawler crwl=new Crawler("exe");
		ArrayList<String> files=crwl.findExeFiles(config.byteCodeAddress);
		return(files);
	}

	private static ArrayList<ArrayList<String>> step2_disAssemble(Configuration config,ArrayList<String> files ) throws Exception
	{
		int ii=0;
		ArrayList<ArrayList<String>> disassebledCodeList=new ArrayList<ArrayList<String>>();
		for(String file : files)
		{
			System.out.println("r"+ii++);
			if(file.contains(" "))
			{
				throw(new Exception("error in disassembler s7fh6fh6fh645"));
			} 
			ArrayList<String> disassebledCode=new ArrayList<String>();
			Runtime rt = Runtime.getRuntime();
			//String command=config.disassembeler_EXE_path+"   "+file + "  /source /text ";
			// String command=config.disassembeler_EXE_path+"   "+file + "  /source /text /linenum ";
			
			
			String command="ildasm "+file + "  /source /text ";
			Process pr = rt.exec(command);
			BufferedReader input = new BufferedReader(new InputStreamReader(pr.getInputStream()));

			String line=null;

			while((line=input.readLine()) != null) {
				//   System.out.println(line);
				disassebledCode.add(line);
			}

			int exitVal = pr.waitFor();
			if(exitVal!=0)
			{
				throw(new Exception("error in disassembler cf45n7zsl4sg4e6"));
			}

			disassebledCodeList.add(disassebledCode);
		}

		return(disassebledCodeList);
	}


	private static void step3_writeDisassembleContentToFiles(Configuration config,ArrayList<String> files,ArrayList<ArrayList<String>> disassembledCodeList,boolean bLineNo) throws Exception
	{

		for(int i=0;i<files.size();i++)
		{
			System.out.println("w"+i);
			String outputFileAddress=config.disassebledAddress+"/"+"XXXXXXXXXXXXXXXXXXXXX";
			File f=new File(files.get(i));
			if(!bLineNo)
			{
				outputFileAddress=config.disassebledAddress+"/"+f.getName()+"_"+i+".txt";
			}else
			{
				outputFileAddress=config.disassebledAddress_lineNo+"/"+f.getName()+"_"+i+".txt";
			}


			BufferedWriter bufferedWriter = null;
			bufferedWriter = new BufferedWriter(new FileWriter(outputFileAddress));
			if(disassembledCodeList.get(i).size()<=0)
			{
				throw(new Exception("error in empty disassembled class!!! 345yvgf346u7w"));
			}
			for(String line : disassembledCodeList.get(i))
			{
				//System.out.println(line);
				bufferedWriter.write(line);
				bufferedWriter.newLine();
			}
			bufferedWriter.flush();
			bufferedWriter.close();


		}
	}

	private static void step4_exportToXML(Configuration config,ArrayList<String> files,ArrayList<ArrayList<String>> disassembledCodeList) throws Exception
	{
		String currentSourceFileAddress="NULL";

		for(int i=0;i< files.size();i++)	
		{

			ArrayList<String> out_lines_binary=new ArrayList<String>();
			ArrayList<String> out_lines_source=new ArrayList<String>();
			ArrayList<String> in_lines=disassembledCodeList.get(i);
			out_lines_binary.add("<project><name></name><description></description><prog_language></prog_language><source_elements>");
			out_lines_source.add("<project><name></name><description></description><prog_language></prog_language><source_elements>");

			ArrayList<String> methodBlockBuffer_binary=new ArrayList<String>();
			ArrayList<String> methodBlockBuffer_source=new ArrayList<String>();
			boolean bInsideMethodBlock=false;
			int methoBlockStartLine=Integer.MAX_VALUE;
			int methoBlockEndLine=Integer.MAX_VALUE;
			for(int j=0;j<in_lines.size();j++)
			{
				if(in_lines.get(j).startsWith("// Source File '"))
				{
					String t=in_lines.get(j).replace("// Source File '", "");
					t=t.substring(0,t.lastIndexOf("'"));
					currentSourceFileAddress=t;
				}

				if(in_lines.get(j).startsWith("  .method"))
				{
					while(!in_lines.get(j).trim().equals("{"))
					{
						j++;
					}
					bInsideMethodBlock=true;
					methoBlockStartLine=Integer.MAX_VALUE;
					methoBlockEndLine=Integer.MAX_VALUE;
				}

				if(in_lines.get(j).startsWith("//"))
				{
					try
					{
						String[] cols= in_lines.get(j).split(":");
						int ln=Integer.parseInt(cols[0].replace("//",""));

						if(bInsideMethodBlock)
						{
							if(methoBlockStartLine==Integer.MAX_VALUE)
							{
								methoBlockStartLine=ln;

							}
							methoBlockEndLine=ln;
						}
					}
					catch(Exception ex)
					{

					}
				}

				if(bInsideMethodBlock && !(in_lines.get(j).startsWith("//")  || in_lines.get(j).trim().startsWith("} // end of method")) )
				{
					methodBlockBuffer_binary.add(in_lines.get(j));
				}

				if(bInsideMethodBlock && in_lines.get(j).startsWith("//")  )
				{

					try
					{
						String[] cols= in_lines.get(j).split(":");
						int ln=Integer.parseInt(cols[0].replace("//",""));

						methodBlockBuffer_source.add(cols[1]);
					}
					catch(Exception ex)
					{

					}


				}

				if(bInsideMethodBlock && in_lines.get(j).trim().startsWith("} // end of method") )
				{
					out_lines_binary.add("<source file=\""+currentSourceFileAddress+"\" startline=\""+methoBlockStartLine+"\" endline=\""+methoBlockEndLine+"\"><![CDATA[");
					out_lines_source.add("<source file=\""+currentSourceFileAddress+"\" startline=\""+methoBlockStartLine+"\" endline=\""+methoBlockEndLine+"\"><![CDATA[");
					for(String llll : methodBlockBuffer_binary)
					{
						out_lines_binary.add(llll);
					}
					for(String llll : methodBlockBuffer_source)
					{
						out_lines_source.add(llll);
					}
					out_lines_binary.add("]]></source>");
					out_lines_source.add("]]></source>");
					methodBlockBuffer_binary.clear();
					methodBlockBuffer_source.clear();
					bInsideMethodBlock=false;
					methoBlockStartLine=Integer.MAX_VALUE;
					methoBlockEndLine=Integer.MAX_VALUE;
				}



			}


			out_lines_binary.add("</source_elements></project>");
			out_lines_source.add("</source_elements></project>");

			writeToXMLFile(config,files.get(i),i,"binary",out_lines_binary);
			writeToXMLFile(config,files.get(i),i,"source",out_lines_source);
		}

	}

	private static void step5_Filter_ExportAllToXML(Configuration config,ArrayList<String> files,ArrayList<ArrayList<String>> disassembledCodeList) throws Exception
	{
		String lin;
		String currentSourceFileAddress="NULL";
		ArrayList<String> out_lines_binary=new ArrayList<String>();
		ArrayList<String> out_lines_source=new ArrayList<String>();

		out_lines_binary.add("<project><name></name><description></description><prog_language></prog_language><source_elements>");
		out_lines_source.add("<project><name></name><description></description><prog_language></prog_language><source_elements>");


		for(int i=0;i< files.size();i++)	
		{			

			ArrayList<String> in_lines=disassembledCodeList.get(i);
			ArrayList<String> methodBlockBuffer_binary=new ArrayList<String>();
			ArrayList<String> methodBlockBuffer_source=new ArrayList<String>();

			boolean bInsideMethodBlock=false;
			int methoBlockStartLine=Integer.MAX_VALUE;
			int methoBlockEndLine=Integer.MAX_VALUE;

			for(int j=0;j<in_lines.size();j++)
			{
				
				if(in_lines.get(j).trim().startsWith("// Source File '"))
				{

					String t=in_lines.get(j).replace("// Source File '", "");
					t=t.substring(0,t.lastIndexOf("'"));
					currentSourceFileAddress=t;
				}

				if(in_lines.get(j).startsWith("  .method"))
				{
					while(!in_lines.get(j).trim().equals("{"))
					{
						j++;
					}
					bInsideMethodBlock=true;
					methoBlockStartLine=Integer.MAX_VALUE;
					methoBlockEndLine=Integer.MAX_VALUE;
				}


				// filtering out .local unit block no need for this filter since we take only sentences started with IL_00
			/*	if(in_lines.get(j).trim().startsWith(".locals init"))
				{
					j++;
					while(!in_lines.get(j).trim().endsWith(")"))
					{
						j++;
					}
				//	j++;  // Bug
				} 
*/

				if(in_lines.get(j).startsWith("//"))
				{
					try
					{
						String[] cols= in_lines.get(j).split(":");
						int ln=Integer.parseInt(cols[0].replace("//",""));
						//System.out.println(ln);

						if(bInsideMethodBlock)
						{
							if(methoBlockStartLine==Integer.MAX_VALUE)
							{
								methoBlockStartLine=ln;
						

							}
							methoBlockEndLine=ln;
						}
					}
					catch(Exception ex)
					{

					}
				}


				if(bInsideMethodBlock && in_lines.get(j).startsWith("//")  )
				{

					try
					{
						String[] cols= in_lines.get(j).split(":");
						int ln=Integer.parseInt(cols[0].replace("//",""));

						methodBlockBuffer_source.add(cols[1]);
					}
					catch(Exception ex)
					{

					}
				}


				//Filtering out these instructions
				if((bInsideMethodBlock && in_lines.get(j).trim().startsWith("IL_") ))
					/* New condition is better than this
					 * 	!(  (in_lines.get(j).trim().startsWith("//") 
								|| (in_lines.get(j).trim().startsWith("} // end of method"))
								|| (in_lines.get(j).trim().startsWith("// Code size"))
								|| (in_lines.get(j).trim().startsWith(".maxstack"))
								|| (in_lines.get(j).trim().startsWith(".language"))
								|| (in_lines.get(j).trim().startsWith(".custom instance"))
								|| (in_lines.get(j).trim().startsWith(".entrypoint"))
								|| (in_lines.get(j).trim().startsWith("{"))))))
								*/
				{


					// filter out the IL_XXXX
					lin=in_lines.get(j);

					if (lin.trim().startsWith("IL_"))
					{
						int w= lin.indexOf(":")+1;
						lin = lin.substring(w);
					}

					// second Filter
					if (lin.trim().startsWith("br.s")||
							lin.trim().startsWith("brtrue.s")||
							lin.trim().startsWith("leave.s")||
							lin.trim().startsWith("blt.s")||
							lin.trim().startsWith("bge.s")||
							lin.trim().startsWith("beq")||
							lin.trim().startsWith("beq.s")||
							lin.trim().startsWith("bge")||
							lin.trim().startsWith("bge.un")||
							lin.trim().startsWith("bge.un.s ")||
							lin.trim().startsWith("bgt")||
							lin.trim().startsWith("bgt.s")||
							lin.trim().startsWith("bgt.un")||
							lin.trim().startsWith("bgt.un.s")||
							lin.trim().startsWith("ble")||
							lin.trim().startsWith("ble.s")||
							lin.trim().startsWith("ble.un")||
							lin.trim().startsWith("ble.un.s")||
							lin.trim().startsWith("blt")||
							lin.trim().startsWith("blt.un")||
							lin.trim().startsWith("blt.un.s")||
							lin.trim().startsWith("bne.un")||
							lin.trim().startsWith("bne.un.s")||			
							lin.trim().startsWith("br")||
							lin.trim().startsWith("brfalse")||	
							lin.trim().startsWith("brfalse.s")||
							lin.trim().startsWith("brnull")||	
							lin.trim().startsWith("brnull.s")||
							lin.trim().startsWith("brzero")||	
							lin.trim().startsWith("brzero.s")||
							lin.trim().startsWith("brtrue")||	
							lin.trim().startsWith("brinst")||
							lin.trim().startsWith("brinst.s")||	
							lin.trim().startsWith("leave"))

					{
						String[] temp=lin.trim().split(" ");
						lin = temp[0];
					}

					
					// filter out all suffixes after the . and all arguments .this is optional filter

					lin=lin.trim();
					int k= lin.indexOf(".");
					if(k>0)
						lin = lin.substring(0,k);

					k=lin.indexOf(" ");
					if(k>0)
							lin = lin.substring(0,k);

						if(lin.trim().startsWith("int")||
								lin.trim().startsWith("float")||
								lin.trim().startsWith("string")||
								lin.trim().startsWith("bool")||
								lin.trim().startsWith("uint")||
								lin.trim().startsWith("IL_")

								)
							continue;

					//end of optional filter
					 
					

					methodBlockBuffer_binary.add(lin);
				}






				if(bInsideMethodBlock && in_lines.get(j).trim().startsWith("} // end of method") )
				{
					// if statement is to filter out small fragments && methods with no source code
					if (methodBlockBuffer_binary.size()>10 && methoBlockStartLine !=Integer.MAX_VALUE && !currentSourceFileAddress.endsWith(".xaml") && methoBlockEndLine-methoBlockStartLine>5)
					
					{
						out_lines_binary.add("<source file=\""+currentSourceFileAddress+"\" startline=\""+methoBlockStartLine+"\" endline=\""+methoBlockEndLine+"\"><![CDATA[");
						for(String llll : methodBlockBuffer_binary)
						{
							out_lines_binary.add(llll);
						}

						out_lines_binary.add("]]></source>");
						
						out_lines_source.add("<source file=\""+currentSourceFileAddress+"\" startline=\""+methoBlockStartLine+"\" endline=\""+methoBlockEndLine+"\"><![CDATA[");
						for(String llll : methodBlockBuffer_source)
						{
					        StringBuffer out = new StringBuffer(); // Used to hold the output.
					        char current; // Used to reference the current character.

					        if (llll == null || ("".equals(llll))) continue;
					        for (int ii = 0; ii < llll.length(); ii++) {
					            current = llll.charAt(ii); // NOTE: No IndexOutOfBoundsException caught here; it should not happen.
					            if ((current == 0x9) ||
					                (current == 0xA) ||
					                (current == 0xD) ||
					                ((current >= 0x20) && (current <= 0xD7FF)) ||
					                ((current >= 0xE000) && (current <= 0xFFFD)) ||
					                ((current >= 0x10000) && (current <= 0x10FFFF)))
					                out.append(current);
					        }
					       
							out_lines_source.add(out.toString());
						}
						out_lines_source.add("]]></source>");
					}


					
					methodBlockBuffer_binary.clear();
					methodBlockBuffer_source.clear();
					bInsideMethodBlock=false;
					methoBlockStartLine=Integer.MAX_VALUE;
					methoBlockEndLine=Integer.MAX_VALUE;
				}

			}

		}

		out_lines_binary.add("</source_elements></project>");
		out_lines_source.add("</source_elements></project>");

		writeToXMLFile(config,"allFiles.xml",00,"binary",out_lines_binary);
		writeToXMLFile(config,"allFiles.xml",00,"source",out_lines_source);

	}

	private static void writeToXMLFile(Configuration config,String filename,int fileID,String format,ArrayList<String> lines) throws Exception
	{


		String outputFileAddress=config.disassebledAddress+"/"+"XXXXXXXXXXXXXXXXXXXXX";

		File f=new File(filename);
		outputFileAddress=config.disassebledAddress+"/"+f.getName()+"_"+fileID+"_"+format+".xml";


		BufferedWriter bufferedWriter = null;
		bufferedWriter = new BufferedWriter(new FileWriter(outputFileAddress));

		for(String line :lines)
		{
			//System.out.println(line);
			bufferedWriter.write(line);
			bufferedWriter.newLine();
		}
		bufferedWriter.flush();
		bufferedWriter.close();


	}
}