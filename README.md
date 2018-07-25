corresponding link is the <<Parsing Techniques 2nd>>, Page 171  


result output:  

input your input string:  
aabc  
replace the front non-terminal:  
1S# ---> AB#  
replace the front non-terminal:  
2S# ---> DC#  
replace the front non-terminal:  
1A1S# ---> aB#  
replace the front non-terminal:  
2A1S# ---> aAB#  
replace the front non-terminal:  
1D2S# ---> abC#  
replace the front non-terminal:  
2D2S# ---> aDbC#  
current analysis:  
1A1S# ---> aB#  
1D2S# ---> abC#  
2A1S# ---> aAB#  
2D2S# ---> aDbC#  
remove front char, a rest: B#  
remove front char, a rest: bC#  
remove front char, a rest: AB#  
remove front char, a rest: DbC#  
replace the front non-terminal:  
1B1A1S# ---> bc#  
replace the front non-terminal:  
2B1A1S# ---> bBc#  
replace the front non-terminal:  
1A2A1S# ---> aB#  
replace the front non-terminal:  
2A2A1S# ---> aAB#  
replace the front non-terminal:  
1D2D2S# ---> abbC#  
replace the front non-terminal:  
2D2D2S# ---> aDbbC#  
current analysis:  
1B1A1S# ---> bc#  
1D2S# ---> bC#  
1A2A1S# ---> aB#  
1D2D2S# ---> abbC#  
2B1A1S# ---> bBc#  
2A2A1S# ---> aAB#  
2D2D2S# ---> aDbbC#  
remove front char, a rest: B#  
remove front char, a rest: bbC#  
remove front char, a rest: AB#  
remove front char, a rest: DbbC#  
remove bad prediction, 1B1A1S#  
remove bad prediction, 1D2S#  
remove bad prediction, 2B1A1S#  
replace the front non-terminal:  
1B1A2A1S# ---> bc#  
replace the front non-terminal:  
2B1A2A1S# ---> bBc#  
replace the front non-terminal:  
1A2A2A1S# ---> aB#  
replace the front non-terminal:  
2A2A2A1S# ---> aAB#  
replace the front non-terminal:  
1D2D2D2S# ---> abbbC#  
replace the front non-terminal:  
2D2D2D2S# ---> aDbbbC#  
current analysis:  
2D2D2D2S# ---> aDbbbC#  
2A2A2A1S# ---> aAB#  
1B1A2A1S# ---> bc#  
1D2D2S# ---> bbC#  
2B1A2A1S# ---> bBc#  
1A2A2A1S# ---> aB#  
1D2D2D2S# ---> abbbC#  
remove front char, b rest: c#  
remove front char, b rest: bC#  
remove front char, b rest: Bc#  
remove bad prediction, 2D2D2D2S#  
remove bad prediction, 2A2A2A1S#  
remove bad prediction, 1A2A2A1S#  
remove bad prediction, 1D2D2D2S#  
replace the front non-terminal:  
1B2B1A2A1S# ---> bcc#  
replace the front non-terminal:  
2B2B1A2A1S# ---> bBcc#  
current analysis:  
1B1A2A1S# ---> c#  
1D2D2S# ---> bC#  
1B2B1A2A1S# ---> bcc#  
2B2B1A2A1S# ---> bBcc#  
remove front char, c rest: #  
remove bad prediction, 1D2D2S#  
remove bad prediction, 1B2B1A2A1S#  
remove bad prediction, 2B2B1A2A1S#  
current analysis:  
1B1A2A1S# ---> #  
remove front char, # rest:  
finded, replace rules: 1B1A2A1S#  
