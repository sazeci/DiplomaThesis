     /////////////////////////////////////////////////////////////////////////////////////
        //go to the left and right(max distance is equal to 2*letter width) and check if: its letter
        private void findBounding(Mat actualImage)
        {
			int startRow;
            int startCollum;
            int startLabel;
            int step = 0;
			//candidates to symbols/symbols
			ArrayList candidates = new ArrayList();
			//check if symbol finded by snail is symbol
			if(checkIfChar(actualLabel) == false){
				//TODO set new coordinates/ probbably middle of the roi	
				Console.WriteLine("No symbol find after snail");
			}
			//first candidate is symbol
			else {				
				candidates.Add(actualLabel);
				startLabel = actualLabel;
				//set start finding row, check is maybe useless
				if (statsImg.Data[startLabel, 1, 0] + (int)statsImg.Data[startLabel, 3, 0] / 2 < actualImage.Height)
				{
					startRow = statsImg.Data[startLabel, 1, 0] + (int)statsImg.Data[startLabel, 3, 0] / 2;
				}
				else {
					startRow = actualImage.Height - 1;
				}
				//start collum
				startCollum = statsImg.Data[startLabel, 0, 0];
				//set bounding
				topRowBB = statsImg.Data[startLabel, 1, 0];
				leftCollumBB = startCollum;
				widthBB = (int)statsImg.Data[startLabel, 2, 0];
				heightBB = (int)statsImg.Data[startLabel, 3, 0];
				if (topRowBB + heightBB < actualImage.Height)
				{
					lowRowBB = topRowBB + heightBB;
				}
				else {
					lowRowBB = actualImage.Height - 1;
				}				
			}
			
        }
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		