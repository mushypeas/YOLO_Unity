convert *.jpg -fuzz 10%% -transparent White -trim _b.png
rm *.jpg
declare -i x=0
for n in *.png; do
    mv -- $n $x.png;
    x=$(($x+1));
done