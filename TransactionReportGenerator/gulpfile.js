"use strict";

var gulp = require('gulp');

var dataFile = './Data.csv';

gulp.task('copyData', function(){
    gulp.src(dataFile)
    .pipe(gulp.dest('./bin/Debug/'));
});

gulp.task('default', ['copyData']);