package levenshtien;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

import simhash.CloneGroup;
import simhash.SourceItem;
import simhash.TestHash2;
import ca.usask.cs.srlab.util.XMLUtil;

import configuration.Configuration;;

public class Levenshtein {

	private static ArrayList<String> vbcode=new ArrayList<String>();
	private static ArrayList<String> cscode=new ArrayList<String>();

	private static ArrayList<ArrayList<String>> vbdata=new ArrayList<ArrayList<String>>();
	private static ArrayList<ArrayList<String>> csdata=new ArrayList<ArrayList<String>>();

	private static final int NEITHER     = 0;
	private static final int UP          = 1;
	private static final int LEFT        = 2;
	private static final int UP_AND_LEFT = 3;

	/**
	 * @param args
	 * Read two xml files and measure similarity between their nodes
	 */
	public static void main(String[] args) throws Exception{
		// TODO Auto-generated method stub



	}

	public static void Leven (String filename)throws Exception	
	{ 
		Configuration config=Configuration.loadFromFile();
		
		String outputFileAddress=config.reportAddress+"\\levenshtienSameCloneReport"+ config.threshold+".xml";
		BufferedWriter bufferedWriter = new BufferedWriter(new FileWriter(outputFileAddress));
		bufferedWriter.write("<clones>");
		bufferedWriter.newLine();

		String outputFileAddress2=config.reportAddress+"\\levenshtienCrossLanguageCloneReport"+ config.threshold+".xml";
		BufferedWriter bufferedWriter2 = new BufferedWriter(new FileWriter(outputFileAddress2));
		bufferedWriter2.write("<clones>");
		bufferedWriter2.newLine();

		parse(filename,filename);

		int nSameLanguage=0;
		int nCrossLanguage=0;

		for(int v =0; v<vbcode.size()-1;v++){
			for(int c =v+1; c<cscode.size();c++){

				if(vbcode.get(v).length()<2000 && cscode.get(c).length()<2000)
				{
					double max= (vbcode.get(v).length()>cscode.get(c).length())? vbcode.get(v).length():cscode.get(c).length();
					double d=(double)1-(getLevenshteinDistance(vbcode.get(v),cscode.get(c))/max);
					//System.out.println(d );
					//out.write(i+"	"+d+"\n");

					if (d>=config.threshold){
						// write to report
						if(vbdata.get(v).get(0).endsWith(".cs")&&csdata.get(c).get(0).endsWith(".vb") || vbdata.get(v).get(0).endsWith(".vb")&&csdata.get(c).get(0).endsWith(".cs")){

							//write to a separate file for cross language clone
							bufferedWriter2.write( "<clone_pair>");
							bufferedWriter2.newLine();
							//System.out.println(d );
							// first fragment
							bufferedWriter2.write( "<clone_fragment file=\""+vbdata.get(v).get(0)+"\" startline=\""+ vbdata.get(v).get(1) +"\" endline=\""+ vbdata.get(v).get(2)+"\">");
							bufferedWriter2.newLine();
							bufferedWriter2.write("<![CDATA["+ getsource( config, vbdata.get(v).get(0), vbdata.get(v).get(1))+"]]>");
							bufferedWriter2.newLine();
							bufferedWriter2.write("</clone_fragment>");
							bufferedWriter2.newLine();
							//second fragment
							bufferedWriter2.write( "<clone_fragment file=\""+csdata.get(c).get(0)+"\" startline=\""+ csdata.get(c).get(1) +"\" endline=\""+ csdata.get(c).get(2)+"\">");
							bufferedWriter2.newLine();
							bufferedWriter2.write("<![CDATA["+getsource( config, csdata.get(c).get(0), csdata.get(c).get(1))+"]]>");
							bufferedWriter2.newLine();
							bufferedWriter2.write("</clone_fragment>");
							bufferedWriter2.newLine();
							//close pair
							bufferedWriter2.write("</clone_pair>");
							bufferedWriter2.newLine();
							nCrossLanguage++;

						}else
						{
							bufferedWriter.write( "<clone_pair>");
							bufferedWriter.newLine();
							//System.out.println(d );
							// first fragment
							bufferedWriter.write( "<clone_fragment file=\""+vbdata.get(v).get(0)+"\" startline=\""+ vbdata.get(v).get(1) +"\" endline=\""+ vbdata.get(v).get(2)+"\">");
							bufferedWriter.newLine();
							bufferedWriter.write("<![CDATA["+ getsource( config, vbdata.get(v).get(0), vbdata.get(v).get(1))+"]]>");
							bufferedWriter.newLine();
							bufferedWriter.write("</clone_fragment>");
							bufferedWriter.newLine();
							//second fragment
							bufferedWriter.write( "<clone_fragment file=\""+csdata.get(c).get(0)+"\" startline=\""+ csdata.get(c).get(1) +"\" endline=\""+ csdata.get(c).get(2)+"\">");
							bufferedWriter.newLine();
							bufferedWriter.write("<![CDATA["+getsource( config, csdata.get(c).get(0), csdata.get(c).get(1))+"]]>");
							bufferedWriter.newLine();
							bufferedWriter.write("</clone_fragment>");
							bufferedWriter.newLine();
							//close pair
							bufferedWriter.write("</clone_pair>");
							bufferedWriter.newLine();
							nSameLanguage++;
						}
					}

				}

			}	 
		}



		// close file		
		bufferedWriter.write("</clones>");
		bufferedWriter.newLine();
		bufferedWriter.flush();
		bufferedWriter.close();		
		
		bufferedWriter2.write("</clones>");
		bufferedWriter2.newLine();
		bufferedWriter2.flush();
		bufferedWriter2.close();

		System.out.println("Report Generated look for file:  levenshtienSameCloneReport.xml");
		System.out.println("and   LCSCrossLanguageCloneReport.xml");
		System.out.println("The number of cross-language clone pairs are: "+nCrossLanguage );
		System.out.println("The number of same-language clone pairs are : "+nSameLanguage);

	}

	public static String getsource(Configuration config, String fileName, String startLine) throws IOException{

		String source=null;
		DocumentBuilderFactory dbfs = DocumentBuilderFactory.newInstance();
		try{
			DocumentBuilder dbs = dbfs.newDocumentBuilder();

			Document docs = dbs.parse(config.disassebledAddress+"\\allFiles.xml_0_source.xml");
			docs.getDocumentElement().normalize();


			Element roots = docs.getDocumentElement();



			NodeList nls = roots.getElementsByTagName("source_elements");

			//		System.out.println(nls.getLength());

			if(nls.getLength()>0){
				NodeList sourceLists = nls.item(0).getChildNodes();



				boolean found=false;
				int k=0;


				while(!found && k<sourceLists.getLength()){
					Node sources = sourceLists.item(k);
					k++;
					if (sources.getNodeType() != Node.ELEMENT_NODE) 
						continue;

					String files = sources.getAttributes().getNamedItem("file").getFirstChild().getNodeValue();
					String startlines = sources.getAttributes().getNamedItem("startline").getFirstChild().getNodeValue();
					//String endlines = sources.getAttributes().getNamedItem("endline").getFirstChild().getNodeValue();
					String contents = sources.getFirstChild().getTextContent();

					if(fileName.equals(files) && startLine.equals(startlines))
					{
						found=true;
						source=contents;

					}

				}
			}



		}

		catch (Exception e) {
			e.printStackTrace();

		}
		return source;
	}


	public static void parse(String cs, String vb ) throws IOException{



		boolean error = false;
		File fileName = new File(cs);
		DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
		try{
			DocumentBuilder db = dbf.newDocumentBuilder();
			Document doc = db.parse(fileName);
			doc.getDocumentElement().normalize();

			Element root = doc.getDocumentElement();

			NodeList nl = root.getElementsByTagName("name");
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
			}

			nl = root.getElementsByTagName("source_elements");
			if(nl.getLength()>0){
				NodeList sourceList = nl.item(0).getChildNodes();

				long start = System.currentTimeMillis();
				long items =0;
				for(int i =0; i < sourceList.getLength(); i++){
					Node source = sourceList.item(i);
					if (source.getNodeType() != Node.ELEMENT_NODE) 
						continue;

					String file = source.getAttributes().getNamedItem("file").getFirstChild().getNodeValue();
					String startline = source.getAttributes().getNamedItem("startline").getFirstChild().getNodeValue();
					String endline = source.getAttributes().getNamedItem("endline").getFirstChild().getNodeValue();
					String content = source.getFirstChild().getTextContent();

					//	int loc = computeLoc(content);

					/*if(content.split("\n").length-1 != loc){
						System.out.println("gotcha!");
					}*/

					ArrayList<String> csd=new ArrayList<String>();
					csd.add(file);
					csd.add(startline);
					csd.add(endline);
					csdata.add(csd);


					String []csA = content.split("\n");
					String temp=null;

					ArrayList<String> csl=new ArrayList<String>();

					for(String t:csA){

						if(t.trim()==null || t.trim().isEmpty())
							continue;
						csl.add(t.trim());
						temp+=t.trim();
					}

					cscode.add(temp);



					/*
					items++;


					System.out.println(i+"================================ "+file);
					System.out.println(" start line = "+startline );
					System.out.println(csl.toString());


					 */
					//System.out.println("node size "+loc);



				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}










		File fileNamevb = new File(vb);


		DocumentBuilderFactory dbfvb = DocumentBuilderFactory.newInstance();
		try{
			DocumentBuilder dbvb = dbfvb.newDocumentBuilder();
			Document docvb = dbvb.parse(fileNamevb);
			docvb.getDocumentElement().normalize();

			Element rootvb = docvb.getDocumentElement();

			NodeList nlvb = rootvb.getElementsByTagName("name");
			if(nlvb.getLength()>0){
				//projectName = nl.item(0).getFirstChild().getNodeValue();
			}else{
				error = true;
			}

			nlvb = rootvb.getElementsByTagName("description");
			if(nlvb.getLength()>0){
				//projectDesc = nl.item(0).getChildNodes().item(0).getNodeValue();
			}else{
				error = true;
			}

			nlvb = rootvb.getElementsByTagName("source_elements");
			if(nlvb.getLength()>0){
				NodeList sourceListvb = nlvb.item(0).getChildNodes();



				for(int j =0; j < sourceListvb.getLength(); j++){
					Node sourcevb = sourceListvb.item(j);
					if (sourcevb.getNodeType() != Node.ELEMENT_NODE) 
						continue;

					String filevb = sourcevb.getAttributes().getNamedItem("file").getFirstChild().getNodeValue();
					String startlinevb = sourcevb.getAttributes().getNamedItem("startline").getFirstChild().getNodeValue();
					String endlinevb = sourcevb.getAttributes().getNamedItem("endline").getFirstChild().getNodeValue();
					String contentvb = sourcevb.getFirstChild().getTextContent();

					//int loc = computeLoc(contentvb);

					/*if(content.split("\n").length-1 != loc){
						System.out.println("gotcha!");
					}*/


					ArrayList<String> vbd=new ArrayList<String>();
					vbd.add(filevb);
					vbd.add(startlinevb);
					vbd.add(endlinevb);
					vbdata.add(vbd);



					String []vbA = contentvb.split("\n");

					ArrayList<String> vbl=new ArrayList<String>();
					String temp=null;

					for(String t:vbA){

						if(t.trim()==null || t.trim().isEmpty())
							continue;
						vbl.add(t.trim());
						temp+=t.trim();
					}

					vbcode.add(temp);


					//	double d= (double)LCSAlgorithm(vbl,csl).size()*2/(vbl.size()+csl.size());

					//	if (d>0.8)
					/*		{

						System.out.println(j+"================================ "+filevb);
						System.out.println(" start line = "+startlinevb );
						System.out.println(vbl.toString());
					}

					 */

					//System.out.println("node size "+loc);


				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}








	}

	private static int computeLoc(String content) {
		String []line = content.split("\n");
		int loc=0;
		for(String ln : line){
			if(ln.length() > 0)
				loc++;
		}

		return loc;
	}


	public static ArrayList<String> LCSAlgorithm(ArrayList<String> a, ArrayList<String> b) 
	{
		int n = a.size();
		int m = b.size();
		int S[][] = new int[n+1][m+1];
		int R[][] = new int[n+1][m+1];
		int ii, jj;

		// It is important to use <=, not <.  The next two for-loops are initialization
		for(ii = 0; ii <= n; ++ii) {
			S[ii][0] = 0;
			R[ii][0] = UP;
		}
		for(jj = 0; jj <= m; ++jj) {
			S[0][jj] = 0;
			R[0][jj] = LEFT;
		}


		// This is the main dynamic programming loop that computes the score and
		// backtracking arrays.
		for(ii = 1; ii <= n; ++ii) {
			for(jj = 1; jj <= m; ++jj) { 

				if( a.get(ii-1).equals(b.get(jj-1)) ) {

					S[ii][jj] = S[ii-1][jj-1] + 1;
					R[ii][jj] = UP_AND_LEFT;
				}

				else {
					S[ii][jj] = S[ii-1][jj-1] + 0;
					R[ii][jj] = NEITHER;
				}

				if( S[ii-1][jj] >= S[ii][jj] ) {	
					S[ii][jj] = S[ii-1][jj];
					R[ii][jj] = UP;
				}

				if( S[ii][jj-1] >= S[ii][jj] ) {
					S[ii][jj] = S[ii][jj-1];
					R[ii][jj] = LEFT;
				}
			}
		}

		// The length of the longest substring is S[n][m]
		ii = n; 
		jj = m;
		int pos = S[ii][jj] - 1;
		ArrayList<String> lcs = new ArrayList<String>();

		// Trace the backtracking matrix.


		while( ii > 0 || jj > 0 ) {
			if( R[ii][jj] == UP_AND_LEFT ) {
				ii--;
				jj--;
				lcs.add(a.get(ii));

			}

			else if( R[ii][jj] == UP ) {
				ii--;
			}

			else if( R[ii][jj] == LEFT ) {
				jj--;
			}
		}

		ArrayList<String> lcs2 = new ArrayList<String>();
		for(int k=lcs.size()-1; k>=0;k--)
			lcs2.add(lcs.get(k));

		return lcs2;
	}

	public static int getLevenshteinDistance (String s, String t) {
		if (s == null || t == null) {
			throw new IllegalArgumentException("Strings must not be null");
		}



		int n = s.length(); // length of s
		int m = t.length(); // length of t

		if (n == 0) {
			return m;
		} else if (m == 0) {
			return n;
		}

		int p[] = new int[n+1]; //'previous' cost array, horizontally
		int d[] = new int[n+1]; // cost array, horizontally
		int _d[]; //placeholder to assist in swapping p and d

		// indexes into strings s and t
		int i; // iterates through s
		int j; // iterates through t

		char t_j; // jth character of t

		int cost; // cost

		for (i = 0; i<=n; i++) {
			p[i] = i;
		}

		for (j = 1; j<=m; j++) {
			t_j = t.charAt(j-1);
			d[0] = j;

			for (i=1; i<=n; i++) {
				cost = s.charAt(i-1)==t_j ? 0 : 1;
				// minimum of cell to the left+1, to the top+1, diagonally left and up +cost
				d[i] = Math.min(Math.min(d[i-1]+1, p[i]+1),  p[i-1]+cost);
			}

			// copy current distance counts to 'previous row' distance counts
			_d = p;
			p = d;
			d = _d;
		}

		// our last action in the above loop was to switch d and p, so p now
		// actually has the most recent cost counts
		return p[n];
	}
}
