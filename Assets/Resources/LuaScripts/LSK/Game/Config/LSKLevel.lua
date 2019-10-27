local LevelConfig = {}
LevelConfig.Normal={
	[1]={id=1,name="第一关",shootcount=8,initspeed={{50,100}},speedchange={{80,150}},changefrequency={0.5,3},cheatfrequency={3,5},cheatspeedchange={{80,150}},cheatrate={},},
	[2]={id=2,name="第二关",shootcount=10,initspeed={{100,200},{-200,-100}},speedchange={{160,260},{-260,-180}},changefrequency={1,2.5},cheatfrequency={2,3},cheatspeedchange={{180,260},{-260,-180}},cheatrate={},},
	[3]={id=3,name="第三关",shootcount=12,initspeed={{180,350},{-350,-180}},speedchange={{280,380},{-380,-280}},changefrequency={0.5,1.5},cheatfrequency={1,2},cheatspeedchange={{280,350},{-350,-220}},cheatrate={100},},
}

LevelConfig.Easy={
	[1]={id=1,name="第一关",shootcount=8,initspeed={{50,100}},speedchange={{80,150}},changefrequency={2,5},cheatfrequency={3,5},cheatspeedchange={{80,150}},cheatrate={},},
	[2]={id=2,name="第二关",shootcount=10,initspeed={{80,150},{-150,-80}},speedchange={{120,220},{-220,-120}},changefrequency={1,5},cheatfrequency={2,3},cheatspeedchange={{180,260},{-260,-180}},cheatrate={},},
	[3]={id=3,name="第三关",shootcount=12,initspeed={{120,260},{-260,-120}},speedchange={{200,300},{-300,-200}},changefrequency={0.5,3},cheatfrequency={1,2},cheatspeedchange={{280,350},{-350,-220}},cheatrate={100},},
}

return LevelConfig