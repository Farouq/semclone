package nicad;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

import configuration.Configuration;
import simhash.SimhashCloneTest6e;


/*
 * this file reads the output report that generated from SimCad and generate our report
 */
public class Report {

	public static void main(String[] args) throws Exception{
		// TODO Auto-generated method stub
		Configuration config=Configuration.loadFromFile();

		//parseSimCad(config, config.reportAddress+"\\asxgui_binary_nicad.xml");
		parseSimCad2(config, config.reportAddress+"\\50_3.xml");



		//        File filesnames = new File("C:/Users/faa634/Documents/similarityunfiltered.txt");
		//       FileWriter out = new FileWriter(filesnames);






	}


	public static void parseSimCad(Configuration config, String rawFunctionsFileName) throws IOException{


		String outputFileAddress=rawFunctionsFileName.subSequence(0, rawFunctionsFileName.lastIndexOf('.'))+"_source.xml";
		int gnfragments=0;
		int vbonly=0;
		int csonly=0;
		int vbw=0;
		int csw=0;
		int vbn=0;
		int csn=0;
		int vbcs=0;


		BufferedWriter bufferedWriter = new BufferedWriter(new FileWriter(outputFileAddress));

		File fileName = new File(rawFunctionsFileName);


		DocumentBuilderFactory dbfs = DocumentBuilderFactory.newInstance();
		DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
		try{
			DocumentBuilder db = dbf.newDocumentBuilder();
			DocumentBuilder dbs = dbfs.newDocumentBuilder();

			Document docs = dbs.parse(config.disassebledAddress+"\\allFiles.xml_0_source.xml");
			docs.getDocumentElement().normalize();
			Document doc = db.parse(fileName);
			doc.getDocumentElement().normalize();

			Element roots = docs.getDocumentElement();
			Element root = doc.getDocumentElement();


			System.out.println(root.getNodeName()+"--------");
			NodeList nl = root.getElementsByTagName("classinfo");
			System.out.println(nl.item(0).getAttributes().getNamedItem("nclasses").getFirstChild().getNodeValue());
			String nclasses=nl.item(0).getAttributes().getNamedItem("nclasses").getFirstChild().getNodeValue();

			bufferedWriter.write("<clones  ngroups=\""+nclasses+"\">");
			bufferedWriter.newLine();

			//System.out.println("number of fragments= "+root.getAttribute("nfragments")+"   ngroups= "+root.getAttribute("ngroups"));
			System.out.println("Csharpe        "+" vb\t"+"Jsharpe\t "+ "CPP\t"+" Fsharpe");


			NodeList nls = roots.getElementsByTagName("source_elements");

			//		System.out.println(nls.getLength());

			if(nls.getLength()>0){
				NodeList sourceLists = nls.item(0).getChildNodes();
				nl = root.getElementsByTagName("class");
				//			 System.out.println(nl.getLength());
				//		 	 System.out.println("groupid"+"	"+"nfragments");

				if(nl.getLength()>0){

					for (int group=0;group<nl.getLength();group++)
					{

						Node groupNode=nl.item(group);


						String groupid = groupNode.getAttributes().getNamedItem("classid").getFirstChild().getNodeValue();
						String nfragments = groupNode.getAttributes().getNamedItem("nclones").getFirstChild().getNodeValue();



						//						System.out.println(groupid+"	"+nfragments);
						gnfragments+=Integer.parseInt(nfragments);

						NodeList sourceList = nl.item(group).getChildNodes();
						//						System.out.println(sourceList.getLength());



						long items =0;

						int cs=0;
						int vb=0;
						int cpp=0;
						int fs=0;
						int js=0;

						ArrayList<ArrayList<String>> sourceCodeData=new ArrayList<ArrayList<String>>();


						for(int i =0; i < sourceList.getLength(); i++){

							Node source = sourceList.item(i);
							if (source.getNodeType() != Node.ELEMENT_NODE) 
								continue;

							String file = source.getAttributes().getNamedItem("file").getFirstChild().getNodeValue();
							String startline = source.getAttributes().getNamedItem("startline").getFirstChild().getNodeValue();
							String endline = source.getAttributes().getNamedItem("endline").getFirstChild().getNodeValue();
							//	String content = source.getFirstChild().getTextContent();
							String g_id = source.getAttributes().getNamedItem("pcid").getFirstChild().getNodeValue();
							/*	
							System.out.println(file);
							System.out.println(startline);
							System.out.println(endline);
						//	System.out.println(content);
							System.out.println(g_id);
							 */

							if( file.endsWith(".cs")) cs++;
							if( file.endsWith(".vb")) vb++;
							if( file.endsWith(".cpp")) cpp++;
							if( file.endsWith(".fs")) fs++;
							if( file.endsWith(".java")) js++;

							boolean found=false;
							int k=0;

							ArrayList<String> current=new ArrayList<String>();

							while(!found && k<sourceLists.getLength()){
								Node sources = sourceLists.item(k);
								k++;
								if (sources.getNodeType() != Node.ELEMENT_NODE) 
									continue;

								String files = sources.getAttributes().getNamedItem("file").getFirstChild().getNodeValue();
								String startlines = sources.getAttributes().getNamedItem("startline").getFirstChild().getNodeValue();
								String endlines = sources.getAttributes().getNamedItem("endline").getFirstChild().getNodeValue();
								String contents = sources.getFirstChild().getTextContent();
								if(file.equals(files) && startline.equals(startlines))
								{
									found=true;

									current.add(files);
									current.add(startlines);
									current.add(endlines);
									current.add(contents);

									// to show the size of byte code vs. source code
									//String []bytecode = content.split("\n");
									//String []sourcecode = contents.split("\n");

									//System.out.println(bytecode.length+"	"+sourcecode.length);
								}
							}


							sourceCodeData.add(current);
							items++;




						}
						// write group	

						bufferedWriter.write( "<clone_group groupid=\""+groupid+"\" nfragments=\""+ nfragments +"\" Csharpe_files=\""+ cs+"\" vb_files=\""+ vb+"\" Jsharpe_files=\""+ js+"\" CPP_files=\""+ cpp+"\" Fsharpe_files=\""+ fs+"\">");
						bufferedWriter.newLine();
						System.out.println( cs+" \t\t"+ vb+" \t "+ js+" \t\t"+ cpp+" \t "+ fs);

						if(cs!=0 && vb!=0)
						{
							vbw+=vb;
							csw+=cs;
							vbcs++;
						}
						if(cs==0 && vb!=0)
						{
							vbonly+=vb;
							vbn++;

						}
						if(cs!=0 && vb==0)
						{
							csonly+=cs;
							csn++;
						}


						for(int z=0;z<sourceCodeData.size();z++)
						{

							bufferedWriter.write( "<clone_fragment file=\""+sourceCodeData.get(z).get(0)+"\" startline=\""+ sourceCodeData.get(z).get(1) +"\" endline=\""+ sourceCodeData.get(z).get(2)+"\">");
							bufferedWriter.newLine();
							bufferedWriter.write("<![CDATA["+sourceCodeData.get(z).get(3)+"]]>");
							bufferedWriter.newLine();
							bufferedWriter.write("</clone_fragment>");
							bufferedWriter.newLine();

						}



						bufferedWriter.write("</clone_group>");
						bufferedWriter.newLine();

					}
					System.out.println("Total nfragment	"+gnfragments);
					System.out.println("vb  "+vbn+"  "+vbonly);
					System.out.println("cs  "+csn+"    "+csonly);
					System.out.println("both	"+vbcs+" n of vb "+vbw+"   n of cs  "+csw);



					// close file		
					bufferedWriter.write("</clones>");
					bufferedWriter.newLine();
					bufferedWriter.flush();
					bufferedWriter.close();		
					System.out.println("Report genarated");



				}
			}



		}

		catch (Exception e) {
			e.printStackTrace();
		}



	}




	public static void parseSimCad2(Configuration config, String rawFunctionsFileName) throws IOException{


		int nfragments=0;




		File fileName = new File(rawFunctionsFileName);


		DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
		try{
			DocumentBuilder db = dbf.newDocumentBuilder();

			Document doc = db.parse(fileName);
			doc.getDocumentElement().normalize();
			Element root = doc.getDocumentElement();

			System.out.println(root.getNodeName()+"--------");

			NodeList nl = root.getElementsByTagName("cloneinfo");

			System.out.println(nl.getLength());
			System.out.println(nl.item(0).getAttributes().getNamedItem("npairs").getFirstChild().getNodeValue());


			nl = root.getElementsByTagName("clone");
			System.out.println(nl.getLength());

			if(nl.getLength()>0){

				for (int group=0;group<nl.getLength();group++)
				{

					NodeList sourceList = nl.item(group).getChildNodes();
					//			
					String file1= sourceList.item(1).getAttributes().getNamedItem("file").getFirstChild().getNodeValue();
					String file2= sourceList.item(3).getAttributes().getNamedItem("file").getFirstChild().getNodeValue();

					if( file1.endsWith(".cs")&&file2.endsWith(".vb") || file1.endsWith(".vb")&&file2.endsWith(".cs")){

						nfragments++;
						System.out.println(group+ "  sssssssssssssssssssssss:  "+nfragments);
					}

				}

				System.out.println("Done");

			}
		}

		catch (Exception e) {
			e.printStackTrace();
		}

	}


}
