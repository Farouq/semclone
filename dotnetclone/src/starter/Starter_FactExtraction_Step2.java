package starter;
import publisher.StarterPublisher;
import configuration.Configuration;
import simhash.SimhashCloneTest6e;
import filters.Filter1;
import filters.Filter2;



public class Starter_FactExtraction_Step2 {

	/**
	 * @param args
	 * @throws Exception 
	 * Push test
	 */
	public static void main(String[] args) throws Exception {

		Configuration config=Configuration.loadFromFile();
		StarterPublisher.start(config);

		switch (config.comparisonMethod)
		{
		case 1:		

			SimhashCloneTest6e.findClone(config.disassebledAddress+"\\allFiles.xml_0_binary.xml");
			break;

		case 2:

			break;
		case 3:
			
			break;
		default:
			
		System.out.println("");
		}

		/*	


		filters.Filter1.filter1(config.disassebledAddress+"\\vb.xml");
		filters.Filter4.filter4(config.disassebledAddress+"\\vb_f1.xml");

		filters.Filter2.filter2(config.disassebledAddress+"\\vb_f1_4.xml");
		filters.Filter4.filter5(config.disassebledAddress+"\\vb_f1_4.xml");
		filters.Filter4.filter6(config.disassebledAddress+"\\vb_f1_4.xml");
		filters.Filter4.filter7(config.disassebledAddress+"\\vb_f1_4.xml");
		filters.Filter4.filter8(config.disassebledAddress+"\\vb_f1_4.xml");
		filters.Filter3.filter3(config.disassebledAddress+"\\vb_f1_4.xml");


		filters.Filter1.filter1(config.disassebledAddress+"\\cs.xml");
		filters.Filter4.filter4(config.disassebledAddress+"\\cs_f1.xml");

		filters.Filter2.filter2(config.disassebledAddress+"\\cs_f1_4.xml");
		filters.Filter4.filter5(config.disassebledAddress+"\\cs_f1_4.xml");
		filters.Filter4.filter6(config.disassebledAddress+"\\cs_f1_4.xml");
		filters.Filter4.filter7(config.disassebledAddress+"\\cs_f1_4.xml");
		filters.Filter4.filter8(config.disassebledAddress+"\\cs_f1_4.xml");
		filters.Filter3.filter3(config.disassebledAddress+"\\cs_f1_4.xml");
		 */
		//		filters.Filter1.filter1(config.disassebledAddress+"\\chap (26).xml");
		//		filters.Filter4.filter4(config.disassebledAddress+"\\chap (26)_f1.xml");
		//		filters.Filter4.filter8(config.disassebledAddress+"\\chap (26)_f1_4.xml");

		//		SimhashCloneTest6e.findClone(config.disassebledAddress+"\\chap (26)_f1_4_8.xml");



		//		for(int i=1;i<=25;i++){
		//	filters.Filter1.filter1(config.disassebledAddress+"\\chap ("+i+").xml");
		//		filters.Filter4.filter4(config.disassebledAddress+"\\chap ("+i+")_f1.xml");
		//		filters.Filter4.filter8(config.disassebledAddress+"\\chap ("+i+")_f1_4.xml");
		/*	filters.Filter4.filter5(config.disassebledAddress+"\\f ("+i+")_f1_4.xml");
		filters.Filter4.filter6(config.disassebledAddress+"\\f ("+i+")_f1_4.xml");
		filters.Filter4.filter7(config.disassebledAddress+"\\f ("+i+")_f1_4.xml");
		filters.Filter4.filter8(config.disassebledAddress+"\\f ("+i+")_f1_4.xml");
		filters.Filter3.filter3(config.disassebledAddress+"\\f ("+i+")_f1_4.xml");
		 */
		//	System.out.println(" file number iiiii"+i+"------------------------------------------------------"+i);
		//	SimhashCloneTest6e.findClone(config.disassebledAddress+"\\chap ("+i+")_f1_4_8.xml");
		//	}
		//	

		//SimhashCloneTest6e.findClone(config.disassebledAddress+"\\chap (15).xml");

		//filters.Filter1.filter(config.disassebledAddress+"\\chap (1).xml");
		//filters.Filter2.filter(config.disassebledAddress+"\\chap (1)_filtered2.xml");
		//filters.Filter3.filter(config.disassebledAddress+"\\chap (1)_filtered2_filtered2.xml");
		//filters.Filter4.filter(config.disassebledAddress+"\\chap (1)_filtered2_filtered2.xml");

	}




}
