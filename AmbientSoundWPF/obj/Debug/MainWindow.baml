   M S B A M L   `   `   `  ����J  FAmbientSoundWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AmbientSoundWPFAmbientSoundWPF  5   	   G(clr-namespace:AmbientSoundWPF.ViewModelsAmbientSoundWPF.ViewModels  5   	   =#clr-namespace:AmbientSoundWPF.ViewsAmbientSoundWPF.Views  5   	        AmbientSoundWPF.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:AmbientSoundWPF     5   	   ?
ViewModels(clr-namespace:AmbientSoundWPF.ViewModels     5   	   5Views#clr-namespace:AmbientSoundWPF.Views     5   	   -   5        $��AmbientSound��5        � Title$  Ambient Sound��5   	    � Icon$ AmbientSound.ico��5      
������� � WindowStartupLocation$ CenterScreen=�6D    � SizeToContent$ WidthAndHeight=�6i    � 
ResizeMode$ CanMinimize=�6�    � 	Resources 5       �� DataType.   (AmbientSoundWPF.ViewModels.HelpViewModel$   AmbientSoundWPF.Views.HelpView � DataContext��%j   (��       5   1   *, +)�� 6
   HelpViewTemplate��"  61   .R�5         6   �� 5      5      .��5      � -   5        $��grid1��5      $��0q�6   ���#312f3c$��Center=�68   $��Center=�6U   $	��700��6p   $	��500��6|    � ColumnDefinitions 5   
   �� 5      $	��24* �6   �� 5      .+�5   
   �� -   5        	 ��Name$	 OnButton��5      $	��OFF��6#   $��Center=�61   $��225,300,0,0q�6N   $��Center=�6c   $	��150��6~   $��50��6�   ���#e43a3a
���   �$��	Helvetica)�6�   $
��Bold"�64  ��6�   Z� ��5      �� 5      
 �� 
TargetType"
 ��6   .�5      ��  �� Property � �� �� Value 5      �� 5        �� 
TargetType" ��6.   .R�5!   "   �� #��z��6)   ._�5"   &   �� $��Center=�67   $��Center=�6T   5#   #   5$      5&       �� Triggers 5'      P� 5(       P� Property �@J�IsMouseOver P� Value . 5*      5,      5.      �� 52   
   -   5        $��MinText��52   k    �� TextAlignment$ Center=�6z   $��24��6�   $��26��6�   $��Center=�6�   $��Center=�6�   $��125,0,0,175q�6�   $��Center=�6�   $��Center=�6  ���#25232f$��DemiBold"�6P  ���#bfbfbf$��Segoe UI Light)�6�  ��6   ��  �� ElementName$ 	MinSlider�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=���6{  Z� 6�  �� 53   
   -   5        $��	MinSlider��53   u    � Value$ 20Q�6    � Maximum$	 100Q�6    �� TickPlacement$ BottomRight=�6,    �� TickFrequency$ 5Q�6H    �� IsSnapToTickEnabled . $	��100��6�   $��26��6�   $��
0,0,25,175q�6�   $��Center=�6�   $��Center=�6�   ���#25232f���#635e78���#25232f6  �� 55   
   -   5        $��MaxText��55   j   $ Center=�6y   $��24��6�   $��26��6�   $��Center=�6�   $��Center=�6�   $��625,0,0,175q�6�   $��Center=�6�   $��Center=�6  ���#25232f$��DemiBold"�6O  ���#bfbfbf$��Segoe UI Light)�6�  ��6   �� $ 	MaxSlider��$ Value�$ PropertyChanged=���6z  Z� 6�  �� 56   
   -   5        $��	MaxSlider��56   u   $ 80Q�6   $	 100Q�6   $ BottomRight=�6,   $ 5Q�6H    . $	��100��6�   $��26��6�   $��475,0,0,175q�6�   $��Center=�6�   $��Center=�6�   ���#25232f���#635e78���#25232f6  �� 58   
   -   5         ��Name$ MinLabel��58      $��Minimum��6"   $��Center=�64   $��
0,0,25,250q�6Q   $��Center=�6e   $��28��6�   ���#bfbfbf$��14(�6�   $
��Bold"�6�   �� 59   
   -	   5        $ MaxLabel��59      $��Maximum��6"   $��Center=�64   $��475,0,0,250q�6Q   $��Center=�6f   $��28��6�   $��DemiBold"�6�   ���#bfbfbf$��14(�6�   �� 5:   
   -
   5        $ InLabel��5:      $��Input��6!   $��Center=�61   $��
0,150,25,0q�6N   $��Center=�6b   $��DemiBold"�6}   ���#bfbfbf$��Segoe UI Semibold)�6�   $��14(�6�   �� 5;   
   -   5        $ OutLabel��5;      $��Output��6"   $��Center=�63   $��475,150,0,0q�6P   $��Center=�6e   $��DemiBold"�6�   ���#bfbfbf$��Segoe UI Semibold)�6�   $��14(�6�   � 5<   
   -   5         �Name$ InLevel��5<      $��Center=�6'   $	��100��6D   $��0,0,25,0q�6Q   $��Center=�6c   $	��100��6~    J� RenderTransformOrigin$ 0.5,0.5&�6�   ��6�   Z� z�5=      V� 5>      .~�5?      �� �� 5@      �� 5A       �� Angle$	 -90Q�6&   T� 5B      5C      5E      � 5F   
   -   5        $ OutLevel��5F      $��Center=�6(   $	��100��6E   $��	475,0,0,0q�6R   $��Center=�6e   $	��100��6�   $ 0.5,0.5&�6�   ��6�   Z� z�5G      V� 5H      .~�5I      �� �� 5J      �� 5K      $	 -90Q�6&   T� 5L      5M      5O      �� 5Q   
   -   5        $��DeviceSelect��5Q      $��Center=�6(   $��225,0,0,350q�6E   $��Center=�6Z   $	��120��6u   ��6�   Z� .@�5R      �� -   5        $��DeviceLabel��5R      $��Center=�6(   $��Center=�6F   $��Select a Device��6a   5S      �� 5T   
   -   5        $��DeviceRefresh��5T      $��Refresh Devices��6&   $��Center=�6@   $��225,0,0,415q�6]   $��Center=�6r   $	��120��6�   ���#25232f���#bfbfbf���	#FF25232F$��Segoe UI Semibold)�6�   $��25��6  ��5U      �� 5V      "
 ��6   .�5X      ��  � �� 5Y      �� 5Z      " ��6.   .R�5[   "   �� #��z��6)   ._�5\   &   �� $��Center=�67   $��Center=�6T   5]   #   5^      5`       5a      P� 5b       �@J�IsMouseOver . 5d      5f      5h      	� 5i   
   -   5        $��IntFast��5i       	� 	GroupName$ Interval��6%   $
��Fast��6;   $��Center=�6J   $��Center=�6g   $��125,400,0,0q�6�   ���#bfbfbf$��Segoe UI Semilight)�6�   ��6�   Z� 6�   	� 5j   
   -   5        $��
IntRelaxed��5j      $ Interval��6(   $��Relaxed��6>   $��Center=�6P   $��Center=�6m   $��300,400,0,0q�6�    d� 	IsChecked$
 TrueY�6�   ���#bfbfbf$��Segoe UI Semilight)�6   ��6�   Z� 6   �� 5l   
   $
��Left=�6   $	��500��61   $	��Top=�6>   $	��200��6V     J� SnapsToDevicePixels  . ���#171523$��2��6�   .+�5m      � .+�5n      �� $��50��6   $��SplashScreen.png��6$   $
��Left=�6>   $��Center=�6Y   �� 5o      $��Ambient Sound��6   $��Bottom=�60   $��20(�6K   $��0,0,5,0q�6Y   ���	#FFF4364A$
��Bold"�6�   $��Nirmala UI Semilight)�6�   $��Right=�6�   5p      �� 5r      -   5        $��
MainButton��5r      $
��MAIN��6'   $��50��66   
�������$��Center=�6U   $��Center=�6r   $	��200��6�   ���	#FFF4364A$��0,5,0,0q�6�   $
��Bold"�6�   ��6�   Z� ��5s      �� 5t      "
 ��6   .�5v      ��  � �� 5w      �� 5x   "   " ��62   .R�5y   &   �� #��z��6-   ._�5z   *   �� $��Center=�6;   $��Center=�6X   5{   '   5|   #   5~       5      P� 5�       �@J�IsMouseOver . 5�      5�      5�      �� 5�      -   5        $��
HelpButton��5�      $
��HELP��6'   $	��200��66   $��50��6B   $��Center=�6N   $��Center=�6k   ���	#FF0B0A1D
�������$��0,5,0,0q�6�   $
��Bold"�6�   ��6�   Z� ��5�      �� 5�      "
 ��6   .�5�      ��  � �� 5�      �� 5�   "   " ��62   .R�5�   &   �� #��z��6-   ._�5�   *   �� $��Center=�6;   $��Center=�6X   5�   '   5�   #   5�       5�      P� 5�       �@J�IsMouseOver . 5�      5�      5�      5�      �� 5�   
   $��0��6   $��2��6&   $��	200,0,0,0q�6N   $ 0.5,0.5&�6a   ��6:   �� z�5�      V� 5�      .~�5�      �� ! �� ScaleX$! 1Q�6%   �� 5�      �� 5�      T� 5�      5�      5�      5�      5�      