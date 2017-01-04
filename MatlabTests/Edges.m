%%% find by edge detectors

%% load
colorImage = imread('6.jpg');
grayMonitor = rgb2gray(colorImage);
%figure;
%imshow(grayMonitor);
title('Image of a Box');

BW = im2bw(grayMonitor,0.5);
figure(1),subplot(2,2,1), imshow(BW)

sizeBW = size(BW,1)*size(BW,2);

%cutting out
BW2 = bwareaopen(BW, floor(sizeBW/50),8);
subplot(2,2,2), imshow(BW2)

%morphological open
se = strel('square',10);
afterOpening = imclose(BW2,se);
subplot(2,2,3), imshow(afterOpening,[]);

%cutting out
BW2 = bwareaopen(afterOpening, floor(sizeBW/50),8);
subplot(2,2,4), imshow(BW2)

linkaxes;

%%
%cutt out nonsense
BW3 = imcomplement(BW2);
BW_out = bwpropfilt(BW3,'Area',5);
BW_out = bwpropfilt(BW_out,'Solidity',1);
figure(2), imshow(BW_out)

%% find edges
edge_canny = edge(grayMonitor, 'Canny');
edge_log = edge(grayMonitor, 'log');
edge_prewitt = edge(grayMonitor, 'Prewitt',0.3);
edge_roberts = edge(grayMonitor, 'Roberts');
edge_sobel = edge(grayMonitor, 'Sobel', [], 'both');
edge_zerocross = edge(grayMonitor, 'zerocross');

%% show
figure(1);
subplot(2,3,1)
imshow(edge_canny)
title('Canny')

subplot(2,3,2)
imshow(edge_log)
title('log')

subplot(2,3,3)
imshow(edge_prewitt)
title('Prewitt')

subplot(2,3,4)
imshow(edge_roberts)
title('Roberts')

subplot(2,3,5)
imshow(edge_sobel)
title('Sobel')

subplot(2,3,6)
imshow(edge_zerocross)
title('zerocross')

linkaxes;

%% fill holes
h = figure(2);

%plot();
imshow(imfill(edge_canny, 'holes'));
















