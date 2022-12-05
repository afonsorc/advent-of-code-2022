# new dir for day
DAY="day$1"
mkdir -p $DAY


# copy contents
cp -R base/. $DAY/


# rename
cd $DAY/src
PROJ_FILE="base.csproj"
NEW_PROJ_FILE="$DAY.csproj"

if [ -f $PROJ_FILE ];
then
    mv "$PROJ_FILE" "$NEW_PROJ_FILE"
fi