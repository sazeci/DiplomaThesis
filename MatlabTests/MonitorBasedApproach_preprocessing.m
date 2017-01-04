%% TEST MONITOR BASED APPROACH
clc; clear all;
tic

colorImage = imread('HC.jpg');
%resize na 300 x neco
%colorImage = imresize(colorImage, [700 NaN]);
imshow(colorImage)
toc
%% color correction
tic
grayMonitor = rgb2gray(colorImage);

%BW = imbinarize(grayMonitor, 0.5);
BW = imbinarize(grayMonitor,'adaptive','Sensitivity',0.6);
%figure(1), imshow(BW)

%cut out ares smaller than 1/50 of image

sizeBW = size(BW,1)*size(BW,2);
%osekani
BW2 = bwareaopen(BW, floor(sizeBW/200),8);
figure(2)
imshow(BW)
toc
%% labelling
tic
complement = imcomplement(BW2);
figure(22), imshow(BW2)
areasMonitor = bwlabel(BW2,8);
areasCorner = bwlabel(complement,8);
figure(3), subplot(1,3,1), imshow(complement)

%%%colloring of areas with contact to border of captured image
%find areas with contact to border of the image
areasEdges = [areasCorner(1,:) areasCorner(size(areasCorner,1),:) areasCorner(:,1)' areasCorner(:,size(areasCorner,2))']; 
areasEdges = unique(areasEdges);

%1 = white
complement(ismember(areasCorner,areasEdges)) = 0;
figure(23), imshow(complement)
%subplot(1,3,2), imshow(complement)
toc
%% 
tic
%find areas with contact to border of the image
areasInMonitor = [areasMonitor(1,:) areasMonitor(size(areasMonitor,1),:) areasMonitor(:,1)' areasMonitor(:,size(areasMonitor,2))']; 
areasInMonitor = unique(areasInMonitor);
%complement

%1 = white
complement(complement == 0 & ~ismember(areasMonitor,areasInMonitor)) = 1;
subplot(1,3,3), imshow(complement)

%keep biggest area
complete = bwpropfilt(complement,'Area',1);
subplot(1,1,1), imshow(complete)
%%% corners
    
    %[I,J]=find(complete>max(complete(:))/2);
    [I,J]=find(complete==1);
    IJ=[I,J];  
    [~,idx]=min(IJ*[1 1; -1 -1; 1 -1; -1 1].');
        %It finds where the sums and differences of the coordinates in the shape are maximized and minimized.
    corners =IJ(idx,:);
        %  corners look like that
            %   1  3
            %   4  2 
    
% count new corners after correction
    width = ((corners(3,2) - corners(1,2))+(corners(2,2) - corners(4,2)))/2;
    height = ((corners(4,1) - corners(1,1))+(corners(2,1) - corners(3,1)))/2;
    BL = corners(4,:);
    TL = [BL(1)-height BL(2)];
    BR = [BL(1) BL(2)+width];
    TR = [TL(1) BR(2)];

    newCorners = [TL; BR; TR; BL];
toc
%% select only screen
tic
cropThing = [min(J), min(I), max(J)-min(J), max(I)-min(I)];%xmin ymin width height
nulledImage = BW;%grayMonitor;
nulledImage(complete == 0) = 0;
croppedGrayScale = imcrop(nulledImage,cropThing);
% figure(4), imshow(croppedGrayScale)
toc    
%% homography
tic
A = corners;
A = [A(:,2) A(:,1)];
B = newCorners;
B = [B(:,2) B(:,1)];
% figure(5)
% imshow(colorImage)
% uiwait(msgbox('Locate the point'));
% [x,y] = ginput(4);
% hold on; % Prevent image from being blown away.
% plot(x,y,'r+', 'MarkerSize', 50);
% plot(A(:,1),A(:,2),'r+', 'MarkerSize', 50);


%# infer projective transformation using CP2TFORM
tform = fitgeotrans(A, B, 'projective');

%apply to image
[outputImage] = imwarp(croppedGrayScale,tform);
figure(6), imshow(outputImage);
toc
%% select only screen
% cut out black borders of the image
outputImage2 = bwareaopen(outputImage, 50,8);

[xWhite, yWhite] = find(outputImage2 == 1);
cropThing = [min(yWhite)-5, min(xWhite)-5, max(yWhite)-min(yWhite)+10, max(xWhite)-min(xWhite)+10];%xmin ymin width height
croppedProjective = imcrop(outputImage2,cropThing);

figure(7), imshow(croppedProjective)

%% cut out bad candidates for digits

%morphological open
% SE = strel('disk', 5);
% croppedProjective = imopen(croppedProjective,se);

%too big bounding for small area
RP = regionprops(croppedProjective,'Extent');
Ex = [RP(:).Extent];
% [mat,padded] = vec2mat(Ex,2);
tooBig = find(Ex < 0.3);
%  || mat(:,2) > size(croppedProjective,2)/4
%labelling
areas = bwlabel(croppedProjective,8);
%cutting out areas
withouBig = croppedProjective;
withouBig(ismember(areas,tooBig)) = 0;

figure(8), imshow(withouBig)

%% selection of candidate
clc
helpa = [0 0 1 0 1;
        0 0 1 0 1;
        0 1 1 0 1;
        0 0 0 0 0
        0 1 0 0 0];
    
% croppedProjective = helpa;

%%% select from image
figure(5)
imshow(croppedProjective)
uiwait(msgbox('Locate the point'));
[x,y] = ginput(1);
hold on; % Prevent image from being blown away.
plot(x,y,'r+', 'MarkerSize', 50);
   
%%% labelling
CC = bwlabel(croppedProjective,8);
%CC = bwconncomp(croppedProjective,8);  
    
%%% select bounding box for candidates
RP = regionprops(CC,'BoundingBox');% parameters L, T corner, width, height
BB = [RP(:).BoundingBox];
[mat,padded] = vec2mat(BB,4);

%select clicked one
selectedArea = CC(ceil(y),ceil(x));

%select candidates in line
distance = 10;
candidatesLine = find(mat(:,2) < mat(selectedArea,2)+distance & mat(:,2) > mat(selectedArea,2)-distance ...
    & mat(:,4) < mat(selectedArea,4)+distance & mat(:,4) > mat(selectedArea,4)-distance);

%select close candidates
% define borders of candidates for selecting candidates on line
boundries = [mat(candidatesLine, 1) mat(candidatesLine,1)+mat(candidatesLine,3)];
correctCandidates = [];
for c = 1:length(candidatesLine)
    %left border
    if(mat(candidatesLine(c),1) - mat(candidatesLine(c),3) > 0)
        leftExpanded = mat(candidatesLine(c),1) - mat(candidatesLine(c),3);
    else
        leftExpanded = 1;
    end    
    %right border
    if(mat(candidatesLine(c),1) + 2*mat(candidatesLine(c),3) < size(CC,2))
        rightExpanded = mat(candidatesLine(c),1) + 2*mat(candidatesLine(c),3);
    else
        rightExpanded = size(CC,2);
    end  
    
    %oblasti od kterych budu hledat hranice uvnitr rozsireneho regionu
    for d = 1:length(candidatesLine)
        if(d == c)
           continue 
        end    
        if(boundries(d,1) > leftExpanded & boundries(d,1) < rightExpanded)
            correctCandidates = [correctCandidates candidatesLine(d)];
            continue
        end      
        if(boundries(d,2) > leftExpanded & boundries(d,2) < rightExpanded)
            correctCandidates = [correctCandidates candidatesLine(d)];
            continue
        end  
    end    
end
correctCandidates = union(unique(correctCandidates), selectedArea);


%keep only cnadidates from line
onlyRegions = croppedProjective;
onlyRegions(~ismember(CC,correctCandidates)) = 0;
figure(6), imshow(onlyRegions)

%% OCR
     ocrResults     = ocr(withouBig)
     recognizedText = ocrResults.Text;


      ocrResults     = ocr(onlyRegions)
      recognizedText = ocrResults.Text;


