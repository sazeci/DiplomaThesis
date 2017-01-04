%% how to find 2d convolution

signal = [ 9 9 9 9 0 0 0 3 6 3 0 0 0 8 8 8 8]
template = [ 3 6 3];
output = conv(signal, template, 'same')
%fajnovejsi
output = normxcorr2(template, signal)

%% test of labelling
helpa = [1 1 1 0 1;
        0 0 0 0 1;
        1 0 1 0 1;
        1 0 0 0 1;
        1 1 1 1 1]
    
BW = im2bw(helpa,0.5);

L = bwlabel(BW,8)

%% histogram cutting test
% 1 = white, 0 = black
colorImage = imread('real.jpg');
grayMonitor = rgb2gray(colorImage);
figure(1), subplot(1,2,1), imshow(grayMonitor)

%imhist(grayMonitor)
BW = imbinarize(grayMonitor,'adaptive','ForegroundPolarity','bright','Sensitivity',0.4);
subplot(1,2,2), imshow(BW)

linkaxes;


%% color spaces
clc; clear all;

colorImage = imread('afterROI.jpg');

figure(1), 
subplot(1,3,1), imshow(colorImage(:,:,1)), title('red')
subplot(1,3,2), imshow(colorImage(:,:,2)), title('green')
subplot(1,3,3), imshow(colorImage(:,:,3)), title('blue')

% HSV
hsv = rgb2hsv(colorImage);
figure(2), 
subplot(1,3,1), imshow(hsv(:,:,1)), title('H')
subplot(1,3,2), imshow(hsv(:,:,2)), title('S')
subplot(1,3,3), imshow(hsv(:,:,3)), title('V')

% XYZ
xyz = rgb2xyz(colorImage);
figure(3), 
subplot(1,3,1), imshow(xyz(:,:,1)), title('X')
subplot(1,3,2), imshow(xyz(:,:,2)), title('Y')
subplot(1,3,3), imshow(xyz(:,:,3)), title('Z')

% konverze na LAB
lab = rgb2xyz(colorImage);
figure(4), 
subplot(1,3,1), imshow(lab(:,:,1)), title('L')
subplot(1,3,2), imshow(lab(:,:,2)), title('A')
subplot(1,3,3), imshow(lab(:,:,3)), title('B')

%show HSV, XYZ, LAB
figure(5), 
subplot(1,3,1), imshow(hsv), title('hsv')
subplot(1,3,2), imshow(xyz), title('xyz')
subplot(1,3,3), imshow(lab), title('lab')

%histograms V from HSV and grayscale
grayMonitor = rgb2gray(colorImage);
%figure(6), 
subplot(1,2,1), imshow(grayMonitor), title('grayscale')
subplot(1,2,2), imshow(hsv(:,:,3)), title('V')
figure(22), imshow(grayMonitor)

figure(7),
subplot(1,2,1), imhist(grayMonitor), title('grayscale')
subplot(1,2,2), imhist(hsv(:,:,3)), title('V')

%binarization test
% figure(8)
% BW = imbinarize(hsv(:,:,2),'adaptive','Sensitivity',0.6);
% imshow(BW);

linkaxes;

%% chars separation
% 1 = white, 0 = black
colorImage = imread('8.jpg');
grayMonitor = rgb2gray(colorImage);
figure(1), subplot(1,2,1), imshow(grayMonitor)

%imhist(grayMonitor)
BW = imbinarize(grayMonitor,'adaptive','ForegroundPolarity','bright','Sensitivity',0.4);
subplot(1,2,2), imshow(BW)

%chars separation (Morphology)
se = strel('line', 3, 90);
afterOpening = imopen(BW,se);
figure(2)
subplot(1,2,1), imshow(BW)
subplot(1,2,2), imshow(afterOpening,[]);

linkaxes;

%% preprocessing bad pixels and contours
colorImage = imread('real.jpg');
grayMonitor = rgb2gray(colorImage);
BW = imbinarize(grayMonitor,'adaptive','ForegroundPolarity','bright','Sensitivity',0.4);
figure(1),
subplot(1,2,1), imshow(BW)

%cut out small ones
BW2 = bwareaopen(BW, 10,8);
subplot(1,2,2), imshow(BW2)


linkaxes;

%% corner detectors
colorImage = imread('HC.jpg');
%colorImage = imresize(colorImage, [200 NaN]);
grayMonitor = rgb2gray(colorImage);
grayMonitor = imgaussfilt(grayMonitor,2);
%BW = grayMonitor;
BW = imbinarize(grayMonitor,'adaptive','Sensitivity',0.4);
figure(1),
subplot(1,3,1), imshow(colorImage)
%title('Original image')

% % classic corner
% C = corner(BW);
% subplot(1,2,2)
% imshow(BW);
% hold on
% plot(C(:,1), C(:,2), 'r*');

% Harris-Stephens
C = detectHarrisFeatures(BW);
subplot(1,3,2)
imshow(BW); hold on;
plot(C.selectStrongest(50));
%title('Harris-Stephens')

%MinEigenFeatures
C = detectMinEigenFeatures(BW);
subplot(1,3,3)
imshow(BW); hold on;
plot(C.selectStrongest(50));
%title('MinEigenFeatures')

linkaxes;

%% finding by region props
clc; clear all;

img = imread('real.jpg');
BW = im2bw(img);

%delete small regions
sizeBW = size(BW,1)*size(BW,2);

%cutting out
BW2 = bwareaopen(BW, floor(sizeBW/50),8);

%morphological open
se = strel('square',10);
afterOpening = imclose(BW2,se);

% find both black and white regions
stats = [regionprops(afterOpening); regionprops(not(afterOpening))]

% show the image and draw the detected rectangles on it
figure(1)
imshow(afterOpening); 
hold on;

for i = 1:numel(stats)
    rectangle('Position', stats(i).BoundingBox, ...
    'Linewidth', 3, 'EdgeColor', 'r', 'LineStyle', '--');
end
%% ------------------------------------------------------------------
%hough
clc; clear all;
I = imread('HC.jpg');
grayMonitor = rgb2gray(I);
BW = edge(grayMonitor,'Canny');
figure(1),
subplot(1,3,1), imshow(BW)

[H,theta,rho] = hough(BW);

figure
imshow(imadjust(mat2gray(H)),[],...
       'XData',theta,...
       'YData',rho,...
       'InitialMagnification','fit');
xlabel('\theta (degrees)')
ylabel('\rho')
axis on
axis normal
hold on
colormap(hot)

P = houghpeaks(H,5,'threshold',ceil(0.3*max(H(:))));

x = theta(P(:,2));
y = rho(P(:,1));
plot(x,y,'s','color','black');

lines = houghlines(BW,theta,rho,P,'FillGap',5,'MinLength',7);

figure, imshow(grayMonitor), hold on
max_len = 0;
for k = 1:length(lines)
   xy = [lines(k).point1; lines(k).point2];
   plot(xy(:,1),xy(:,2),'LineWidth',2,'Color','green');

   % Plot beginnings and ends of lines
   plot(xy(1,1),xy(1,2),'x','LineWidth',2,'Color','yellow');
   plot(xy(2,1),xy(2,2),'x','LineWidth',2,'Color','red');

   Determine the endpoints of the longest line segment
   len = norm(lines(k).point1 - lines(k).point2);
   if ( len > max_len)
      max_len = len;
      xy_long = xy;
   end
end
% highlight the longest line segment
plot(xy_long(:,1),xy_long(:,2),'LineWidth',2,'Color','red');

%% histogram equalization
clc; clear all

I = imread('gray.jpg');
grayMonitor = rgb2gray(I);
figure(1); 
imshow(grayMonitor);
figure(2);
[pixelCount, grayLevels] = imhist(grayMonitor,64);
bar(grayLevels, pixelCount);
grid on;
xlabel('Graylevel', 'FontSize', 11);
ylabel('Number of pixels', 'FontSize', 11);
xlim([0 grayLevels(end)]); % Scale x axis manually.



J = histeq(grayMonitor);
figure(3);
imshow(J)
figure(4);
[pixelCount, grayLevels] = imhist(J,64);
bar(grayLevels, pixelCount);
grid on;
xlabel('Graylevel', 'FontSize', 11);
ylabel('Number of pixels', 'FontSize', 11);
xlim([0 grayLevels(end)]); % Scale x axis manually.

%% filtering
I = imread('afterROI_4.jpg');
grayMonitor = rgb2gray(I);
%low pass by convolution
% boxKernel = ones(21,21); % Or whatever size window you want.
% blurredImage = conv2(grayMonitor, boxKernel, 'same');
% figure(2), imshow(blurredImage)

%gaussian
figure(1)
subplot(1,3,1)
H = fspecial('gaussian',5,5);
MotionBlur = imfilter(grayMonitor,H,'replicate');
imshow(MotionBlur);

%average
subplot(1,3,2)
H = fspecial('average');
MotionBlur = imfilter(grayMonitor,H,'replicate');
imshow(MotionBlur);

% median
subplot(1,3,3)
B = medfilt2(grayMonitor, [5 5]);
imshow(B)

%% MSER
% [https://www.mathworks.com/help/vision/examples/automatically-detect-and-recognize-text-in-natural-images.html]
I = imread('gray.jpg');
grayMonitor = rgb2gray(I);

% Detect MSER regions.
[mserRegions, mserConnComp] = detectMSERFeatures(grayMonitor, ...
    'RegionAreaRange',[200 8000],'ThresholdDelta',4);

figure
imshow(grayMonitor)
hold on
plot(mserRegions, 'showPixelList', true,'showEllipses',false)
title('MSER regions')
hold off


% Use regionprops to measure MSER properties
mserStats = regionprops(mserConnComp, 'BoundingBox', 'Eccentricity', ...
    'Solidity', 'Extent', 'Euler', 'Image');

% Compute the aspect ratio using bounding box data.
bbox = vertcat(mserStats.BoundingBox);
w = bbox(:,3);
h = bbox(:,4);
aspectRatio = w./h;

% Threshold the data to determine which regions to remove. These thresholds
% may need to be tuned for other images.
filterIdx = aspectRatio' > 3;
filterIdx = filterIdx | [mserStats.Eccentricity] > .995 ;
filterIdx = filterIdx | [mserStats.Solidity] < .3;
filterIdx = filterIdx | [mserStats.Extent] < 0.2 | [mserStats.Extent] > 0.9;
filterIdx = filterIdx | [mserStats.EulerNumber] < -4;

% Remove regions
mserStats(filterIdx) = [];
mserRegions(filterIdx) = [];

% Show remaining regions
figure
imshow(I)
hold on
plot(mserRegions, 'showPixelList', true,'showEllipses',false)
title('After Removing Non-Text Regions Based On Geometric Properties')
hold off


% Get a binary image of the a region, and pad it to avoid boundary effects
% during the stroke width computation.
regionImage = mserStats(6).Image;
regionImage = padarray(regionImage, [1 1]);

% Compute the stroke width image.
distanceImage = bwdist(~regionImage);
skeletonImage = bwmorph(regionImage, 'thin', inf);

strokeWidthImage = distanceImage;
strokeWidthImage(~skeletonImage) = 0;

% Show the region image alongside the stroke width image.
figure
subplot(1,2,1)
imagesc(regionImage)
title('Region Image')

subplot(1,2,2)
imagesc(strokeWidthImage)
title('Stroke Width Image')

% Compute the stroke width variation metric
strokeWidthValues = distanceImage(skeletonImage);
strokeWidthMetric = std(strokeWidthValues)/mean(strokeWidthValues);

% Threshold the stroke width variation metric
strokeWidthThreshold = 0.4;
strokeWidthFilterIdx = strokeWidthMetric > strokeWidthThreshold;

% Process the remaining regions
for j = 1:numel(mserStats)

    regionImage = mserStats(j).Image;
    regionImage = padarray(regionImage, [1 1], 0);

    distanceImage = bwdist(~regionImage);
    skeletonImage = bwmorph(regionImage, 'thin', inf);

    strokeWidthValues = distanceImage(skeletonImage);

    strokeWidthMetric = std(strokeWidthValues)/mean(strokeWidthValues);

    strokeWidthFilterIdx(j) = strokeWidthMetric > strokeWidthThreshold;

end

% Remove regions based on the stroke width variation
mserRegions(strokeWidthFilterIdx) = [];
mserStats(strokeWidthFilterIdx) = [];

% Show remaining regions
figure
imshow(I)
hold on
plot(mserRegions, 'showPixelList', true,'showEllipses',false)
title('After Removing Non-Text Regions Based On Stroke Width Variation')
hold off

% Get bounding boxes for all the regions
bboxes = vertcat(mserStats.BoundingBox);

% Convert from the [x y width height] bounding box format to the [xmin ymin
% xmax ymax] format for convenience.
xmin = bboxes(:,1);
ymin = bboxes(:,2);
xmax = xmin + bboxes(:,3) - 1;
ymax = ymin + bboxes(:,4) - 1;

% Expand the bounding boxes by a small amount.
expansionAmount = 0.02;
xmin = (1-expansionAmount) * xmin;
ymin = (1-expansionAmount) * ymin;
xmax = (1+expansionAmount) * xmax;
ymax = (1+expansionAmount) * ymax;

% Clip the bounding boxes to be within the image bounds
xmin = max(xmin, 1);
ymin = max(ymin, 1);
xmax = min(xmax, size(I,2));
ymax = min(ymax, size(I,1));

% Show the expanded bounding boxes
expandedBBoxes = [xmin ymin xmax-xmin+1 ymax-ymin+1];
IExpandedBBoxes = insertShape(colorImage,'Rectangle',expandedBBoxes,'LineWidth',3);

figure
imshow(IExpandedBBoxes)
title('Expanded Bounding Boxes Text')

% Compute the overlap ratio
overlapRatio = bboxOverlapRatio(expandedBBoxes, expandedBBoxes);

% Set the overlap ratio between a bounding box and itself to zero to
% simplify the graph representation.
n = size(overlapRatio,1);
overlapRatio(1:n+1:n^2) = 0;

% Create the graph
g = graph(overlapRatio);

% Find the connected text regions within the graph
componentIndices = conncomp(g);

% Merge the boxes based on the minimum and maximum dimensions.
xmin = accumarray(componentIndices', xmin, [], @min);
ymin = accumarray(componentIndices', ymin, [], @min);
xmax = accumarray(componentIndices', xmax, [], @max);
ymax = accumarray(componentIndices', ymax, [], @max);

% Compose the merged bounding boxes using the [x y width height] format.
textBBoxes = [xmin ymin xmax-xmin+1 ymax-ymin+1];

% Remove bounding boxes that only contain one text region
numRegionsInGroup = histcounts(componentIndices);
textBBoxes(numRegionsInGroup == 1, :) = [];

% Show the final text detection result.
ITextRegion = insertShape(colorImage, 'Rectangle', textBBoxes,'LineWidth',3);

figure
imshow(ITextRegion)
title('Detected Text')

ocrtxt = ocr(I, textBBoxes);
[ocrtxt.Text]

%% thresholding
clc; clear all;
colorImage = imread('beforeROi.jpg');
grayMonitor = rgb2gray(colorImage);
figure(1); imshow(grayMonitor)
% 
% %otsu
% figure(1)
% level = graythresh(grayMonitor);
% BW = im2bw(grayMonitor,level);
% imshow(BW)
% 
% %classic
% figure(2)
% BW = im2bw(grayMonitor,0.6);%set by user
% imshow(BW)

%adaptive gaussian
level = adaptthresh(grayMonitor,'NeighborhoodSize', 101, 'Statistic','gaussian');
BW = imbinarize(grayMonitor,level);
figure(3)
imshow(BW)

%% resize to small 5*4
clc; clear all;
colorImage = imread('8.jpeg');
colorImage = imresize(colorImage, [5 4]);

level = graythresh(colorImage);
BW = im2bw(colorImage,level);
imshow(BW)

imwrite(BW,'colorImage.jpeg')

%% diference
clc; clear all;
A = imread('35x35.jpg');
B = imread('35new.jpg');

C = imabsdiff(A,B);
figure(1); imshow(C)

se = strel('square',2);
D = imopen(C,se);
figure(2)
imshow(D);

%% invert image

colorImage = imread('graphtest.jpg');
IM2 = imcomplement(colorImage);
imshow(IM2)



