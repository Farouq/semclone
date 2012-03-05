package publisher;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
 
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.HashSet;

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



}