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
            String command=config.disassembeler_EXE_path+"   "+file + "  /source /text ";
           // String command=config.disassembeler_EXE_path+"   "+file + "  /source /text /linenum ";
         
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
		String currentSourceFileAddress="NULL";
		ArrayList<String> out_lines_binary=new ArrayList<String>();
		out_lines_binary.add("<project><name></name><description></description><prog_language></prog_language><source_elements>");

		for(int i=0;i< files.size();i++)	
		{			
			
			ArrayList<String> in_lines=disassembledCodeList.get(i);
			ArrayList<String> methodBlockBuffer_binary=new ArrayList<String>();
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
				
				if((bInsideMethodBlock && !(in_lines.get(j).startsWith("//")  || in_lines.get(j).trim().startsWith("} // end of method")))
						 && !(in_lines.get(j).trim().startsWith("// Code size")) && !(in_lines.get(j).trim().startsWith(".maxstack"))
						 && !(in_lines.get(j).trim().startsWith(".language")) && !(in_lines.get(j).trim().startsWith(".custom instance"))
						 && !(in_lines.get(j).trim().startsWith(".entrypoint")))
				{
					
					if(in_lines.get(j).trim().startsWith(".locals init"))
					{
						while(!in_lines.get(j).trim().endsWith(")"))
						{
							j++;
						}
						j++;
					} 
					
					methodBlockBuffer_binary.add(in_lines.get(j));
				}
				
							
				
				if(bInsideMethodBlock && in_lines.get(j).trim().startsWith("} // end of method") )
				{
					out_lines_binary.add("<source file=\""+currentSourceFileAddress+"\" startline=\""+methoBlockStartLine+"\" endline=\""+methoBlockEndLine+"\"><![CDATA[");
					for(String llll : methodBlockBuffer_binary)
					{
						out_lines_binary.add(llll);
					}
					
					out_lines_binary.add("]]></source>");
					
					methodBlockBuffer_binary.clear();
					bInsideMethodBlock=false;
					methoBlockStartLine=Integer.MAX_VALUE;
					methoBlockEndLine=Integer.MAX_VALUE;
				}
								
			}
		
		}
		
		out_lines_binary.add("</source_elements></project>");
		
		writeToXMLFile(config,"allFiles.xml",00,"binary",out_lines_binary);
		
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