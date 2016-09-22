var gulp = require('gulp'),
    sass = require('gulp-sass'),
    cssnano = require('gulp-cssnano'),
    uglify = require('gulp-uglify'),
    rename = require('gulp-rename'),
    concat = require('gulp-concat'),
    del = require('del');


var config = require('./gulp-config.json');
var assets = config.paths.assets;
var source = config.paths.source;


gulp.task('vendor-scripts', function () {
    return gulp.src(config.vendorPaths.js)
        .pipe(gulp.dest(source + '/js/'));
});

gulp.task('vendor-styles', function () {
    return gulp.src(config.vendorPaths.css)
        .pipe(rename({ prefix: '_' }))
        .pipe(rename({ extname: '.scss' }))
        .pipe(gulp.dest(source + '/sass/vendor/'));
});

gulp.task('vendor-fonts', function () {
    return gulp.src(config.vendorPaths.fonts)
        .pipe(gulp.dest(source + '/fonts/'));
});


gulp.task('scripts', function () {
    return gulp.src(config.paths.js)
        .pipe(concat('main.js'))
        .pipe(gulp.dest(assets + '/js/'))
        .pipe(rename({ suffix: '.min' }))
        .pipe(uglify())
        .pipe(gulp.dest(assets + '/js/'));
});

gulp.task('styles', function () {
    return gulp.src(config.paths.css)
        .pipe(sass().on('error', sass.logError))
        .pipe(concat('main.css'))
        .pipe(gulp.dest(assets + '/css/'))
        .pipe(rename({ suffix: '.min' }))
        .pipe(cssnano())
        .pipe(gulp.dest(assets + '/css/'));
});

gulp.task('fonts', function () {
    return gulp.src(config.paths.fonts)
        .pipe(gulp.dest(assets + '/fonts/'));
});

gulp.task('images', function () {
    return gulp.src(config.paths.images)
        .pipe(gulp.dest(assets + '/images/'));
});


// Clean
gulp.task('clean', function () {
    return del([assets + '/css', assets + '/js', assets + '/fonts', assets + '/images']);
});

// Default task
gulp.task('default', ['clean'], function () {
    gulp.start('styles', 'scripts', 'fonts', 'images');
});