# new dir for day
DAY="day$1"
mkdir -p $DAY


# copy contents
cp -R base/. $DAY/
cd $DAY/src