
SESSION="53616c7465645f5f6a50d3d733d4b46eb55137cb3ddfc2b8dbfc5111085693f5ca179519be94e39304bc8e636824463565ec035ee1404f6965be803a1c5ffdc9"
USER_AGENT="github.com/afonsorc/advent-of-code-2022 by afonsorc@outlook.com"

YEAR="2022"
DAY=$1
DAY_FIXED=$DAY

# fix day variable
LEN=`echo -n $DAY | wc -c`
if [ $LEN == 1 ];
then
    DAY_FIXED="0$DAY"
fi

DAY_FOLDER="day$DAY_FIXED"


# new dir for day
mkdir -p $DAY_FOLDER


# copy contents
cp -R base/. $DAY_FOLDER/


# get input file
cd $DAY_FOLDER/input
curl -A $USER_AGENT https://adventofcode.com/$YEAR/day/$DAY/input --cookie "session=$SESSION" > input.txt


# rename
cd ../src
PROJ_FILE="base.csproj"
NEW_PROJ_FILE="$DAY_FOLDER.csproj"

if [ -f $PROJ_FILE ];
then
    mv "$PROJ_FILE" "$NEW_PROJ_FILE"
fi