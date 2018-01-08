#! /bin/sh
FILE=$1
FILE_WITH_OUT_EXT=${FILE%%.*}
./resize.sh $FILE
./compress.sh $FILE
./file_to_bytes.py $FILE_WITH_OUT_EXT-or8.png
