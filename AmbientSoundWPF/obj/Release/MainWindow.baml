   M S B A M L   `   `   `  ���� J  FAmbientSoundWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AmbientSoundWPFAmbientSoundWPF  G(clr-namespace:AmbientSoundWPF.ViewModelsAmbientSoundWPF.ViewModels  =#clr-namespace:AmbientSoundWPF.ViewsAmbientSoundWPF.Views       AmbientSoundWPF.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:AmbientSoundWPF     ?
ViewModels(clr-namespace:AmbientSoundWPF.ViewModels     5Views#clr-namespace:AmbientSoundWPF.Views     -   $��AmbientSound��  � Title$  Ambient Sound�� � Icon$ AmbientSound.ico��
������� � WindowStartupLocation$ CenterScreen=� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ CanMinimize=� � 	Resources  �� DataType.   (AmbientSoundWPF.ViewModels.HelpViewModel$   AmbientSoundWPF.Views.HelpView � DataContext��%@   (��       *, +)�� HelpViewTemplate��"  .R�   �� .��� -   $��grid1��$��0q����#312f3c$��Center=�$��Center=�$	��700��$	��500�� � ColumnDefinitions �� $	��24* ��� .+��� -   	 ��Name$	 OnButton��$	��OFF��$��Center=�$��225,300,0,0q�$��Center=�$	��150��$��50�����#e43a3a
���   �$��	Helvetica)�$
��Bold"���Z� ���� 
 �� 
TargetType"
 ��.���  �� Property � �� �� Value ��  �� 
TargetType" ��.R��� #��z��._��� $��Center=�$��Center=� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . �� -   $��MinText�� �� TextAlignment$ Center=�$��24��$��26��$��Center=�$��Center=�$��125,0,0,175q�$��Center=�$��Center=����#25232f$��DemiBold"����#bfbfbf$��Segoe UI Light)�����  �� ElementName$ 	MinSlider�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=���Z� �� -   $��	MinSlider�� � Value$ 20Q� � Maximum$	 100Q� �� TickPlacement$ BottomRight=� �� TickFrequency$ 5Q� �� IsSnapToTickEnabled . $	��100��$��26��$��
0,0,25,175q�$��Center=�$��Center=����#25232f���#635e78���#25232f�� -   $��MaxText��$ Center=�$��24��$��26��$��Center=�$��Center=�$��625,0,0,175q�$��Center=�$��Center=����#25232f$��DemiBold"����#bfbfbf$��Segoe UI Light)����� $ 	MaxSlider��$ Value�$ PropertyChanged=���Z� �� -   $��	MaxSlider��$ 80Q�$	 100Q�$ BottomRight=�$ 5Q� . $	��100��$��26��$��475,0,0,175q�$��Center=�$��Center=����#25232f���#635e78���#25232f�� -    ��Name$ MinLabel��$��Minimum��$��Center=�$��
0,0,25,250q�$��Center=�$��28�����#bfbfbf$��14(�$
��Bold"��� -	   $ MaxLabel��$��Maximum��$��Center=�$��475,0,0,250q�$��Center=�$��28��$��DemiBold"����#bfbfbf$��14(��� -
   $ InLabel��$��Input��$��Center=�$��
0,150,25,0q�$��Center=�$��DemiBold"����#bfbfbf$��Segoe UI Semibold)�$��14(��� -   $ OutLabel��$��Output��$��Center=�$��475,150,0,0q�$��Center=�$��DemiBold"����#bfbfbf$��Segoe UI Semibold)�$��14(�� -    �Name$ InLevel��$��Center=�$	��100��$��0,0,25,0q�$��Center=�$	��100�� J� RenderTransformOrigin$ 0.5,0.5&���Z� z�V� .~��� �� ��  �� Angle$	 -90Q�T� � -   $ OutLevel��$��Center=�$	��100��$��	475,0,0,0q�$��Center=�$	��100��$ 0.5,0.5&���Z� z�V� .~��� �� �� $	 -90Q�T� �� -   $��DeviceSelect��$��Center=�$��225,0,0,350q�$��Center=�$	��120����Z� .@��� -   $��DeviceLabel��$��Center=�$��Center=�$��Select a Device���� -   $��DeviceRefresh��$��Refresh Devices��$��Center=�$��225,0,0,415q�$��Center=�$	��120�����#25232f���#bfbfbf���	#FF25232F$��Segoe UI Semibold)�$��25������ "
 ��.���  � �� �� " ��.R��� #��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . 	� -   $��IntFast�� 	� 	GroupName$ Interval��$
��Fast��$��Center=�$��Center=�$��125,400,0,0q����#bfbfbf$��Segoe UI Semilight)���Z� 	� -   $��
IntRelaxed��$ Interval��$��Relaxed��$��Center=�$��Center=�$��300,400,0,0q� d� 	IsChecked$
 TrueY����#bfbfbf$��Segoe UI Semilight)���Z� �� $
��Left=�$	��500��$	��Top=�$	��200��  J� SnapsToDevicePixels  . ���#171523$��2��.+�� .+��� $��50��$��SplashScreen.png��$
��Left=�$��Center=��� $��Ambient Sound��$��Bottom=�$��20(�$��0,0,5,0q����	#FFF4364A$
��Bold"�$��Nirmala UI Semilight)�$��Right=��� -   $��
MainButton��$
��MAIN��$��50��
�������$��Center=�$��Center=�$	��200�����	#FFF4364A$��0,5,0,0q�$
��Bold"���Z� ���� "
 ��.���  � �� �� " ��.R��� #��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . �� -   $��
HelpButton��$
��HELP��$	��200��$��50��$��Center=�$��Center=����	#FF0B0A1D
�������$��0,5,0,0q�$
��Bold"���Z� ���� "
 ��.���  � �� �� " ��.R��� #��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . �� $��0��$��2��$��	200,0,0,0q�$ 0.5,0.5&����� z�V� .~��� ! �� ScaleX$! 1Q��� �� T� 