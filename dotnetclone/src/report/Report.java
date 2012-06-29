package report;

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

		parseSimCad(config, config.disassebledAddress+"\\allFiles.xml_0-secco_clones-multi_index-"+SimhashCloneTest6e.simThreshold+".xml");



		//        File filesnames = new File("C:/Users/faa634/Documents/similarityunfiltered.txt");
		//       FileWriter out = new FileWriter(filesnames);






	}


	public static void parseSimCad(Configuration config, String rawFunctionsFileName) throws IOException{


		String outputFileAddress=config.reportAddress+"CloneReportt.xml";

	
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


			bufferedWriter.write("<clones  nfragments=\""+root.getAttribute("nfragments")+"\" ngroups=\""+root.getAttribute("ngroups")+"\">");
			bufferedWriter.newLine();

			System.out.println("number of fragments= "+root.getAttribute("nfragments")+"   ngroups= "+root.getAttribute("ngroups"));
			System.out.println("Csharpe        "+" vb\t"+"Jsharpe\t "+ "CPP\t"+" Fsharpe");

			// start file

			/*	NodeList nl = root.getElementsByTagName("name");
			if(nl.getLength()>0){
				//projectName = nl.item(0).getFirstChild().getNodeValue();
			}else{
				error = true;
			}

			nl = root.getElementsByTagName("description");
			if(nl.getLength()>0){
				//projectDesc = nl.item(0).getChildNodes().item(0).getNodeValue();
			}else{
				error = true;
			}*/

			NodeList nls = roots.getElementsByTagName("source_elements");
			if(nls.getLength()>0){
				NodeList sourceLists = nls.item(0).getChildNodes();
				NodeList nl = root.getElementsByTagName("clone_group");

				if(nl.getLength()>0){

					for (int group=0;group<nl.getLength();group++)
					{

						Node groupNode=nl.item(group);


						String groupid = groupNode.getAttributes().getNamedItem("groupid").getFirstChild().getNodeValue();
						String nfragments = groupNode.getAttributes().getNamedItem("nfragments").getFirstChild().getNodeValue();
						


						NodeList sourceList = nl.item(group).getChildNodes();


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
							String content = source.getFirstChild().getTextContent();
							String g_id = source.getAttributes().getNamedItem("pcid").getFirstChild().getNodeValue();

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



}
