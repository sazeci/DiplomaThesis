README

This CD contain standalone application in C#, project with this application, text of Diploma Thesis, results of experiment and tests in Matlab.

StandAloneApplication_x64
- This folder contain standalone application for windows systems with x64 architecture
- All necessary libraries are included
- For successful start of this application it’s necessary to install .NET Framework 4.5.2
- To start application please use file Diploma.exe
- After the start please follow instructions in the application
- File with acquire data is localize in this folder under user defined name
	Tessdata
	- This folder contain train data for Tesseract
	TemplatesTestMonitor
	- This folder contains mask for classifying by Difference with stored masks

ProjectWithApplication
- This folder contain whole project with implemented application
- Application was implemented in Visual studio 2015 (Community edition)

Text
- This folder contain text of diploma thesis 
- Text is in two formats docx and pdf

MatlabTest
- This folder contain all material implemented in Matlab and attached pictures for testing 
	Edges.m
	- This file contain tests with edge detectors
	findByTemplate.m
	- This file trying to find monitor in scene by interesting points in template
	MonitorBasedApproach%preprocessing.m
	- This file contain simplified monitor based approach
	- Monitor in scene is found by preprocessing
	test.m
	- This file contains almost all done test on images with patient monitor
	- These test correspond to methods mentioned in Diploma thesis
			
ResultsOfExperiments
- This folder contain file with all outcomes of all experiments
- File is in format xlsx, and it’s called ExperimentsResults.xlsx

