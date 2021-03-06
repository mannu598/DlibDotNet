﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using DlibDotNet.Extensions;
using uint8_t = System.Byte;
using uint16_t = System.UInt16;
using uint32_t = System.UInt32;
using int8_t = System.SByte;
using int16_t = System.Int16;
using int32_t = System.Int32;

namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static Rectangle GetRect(HoughTransform houghTransform)
        {
            if (houghTransform == null)
                throw new ArgumentNullException(nameof(houghTransform));

            houghTransform.ThrowIfDisposed();

            HoughTransform.Native.hough_transform_get_rect(houghTransform.NativePtr, out var rect);
            return new Rectangle(rect);
        }

        public static Array2D<T> LoadBmp<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException("", path);

            var str = Encoding.UTF8.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.load_bmp(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return image;
        }

        public static Array2D<T> LoadDng<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Encoding.UTF8.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.load_dng(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return image;
        }

        public static Array2D<T> LoadImage<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Encoding.UTF8.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.load_image(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return image;
        }

        public static Array2D<T> LoadJpeg<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Encoding.UTF8.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.load_jpeg(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return image;
        }

        public static Array2D<T> LoadPng<T>(string path)
            where T : struct
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Encoding.UTF8.GetBytes(path);

            var image = new Array2D<T>();

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.load_png(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");

            return image;
        }

        public static void SaveBmp(Array2DBase image, string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            // NOTE: save_bmp does not throw excpetion but it does NOT output any file.
            //       So it should throw exception in this timing!!
            if (image.Rows <= 0 || image.Columns <= 0)
                throw new ArgumentException($"{nameof(image.Columns)} and {nameof(image.Rows)} is less than or equal to zero.", nameof(image));

            var str = Encoding.UTF8.GetBytes(path);

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.save_bmp(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");
        }

        public static void SaveDng(Array2DBase image, string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            // NOTE: save_dng does not throw excpetion but it does NOT output any file.
            //       So it should throw exception in this timing!!
            if (image.Rows <= 0 || image.Columns <= 0)
                throw new ArgumentException($"{nameof(image.Columns)} and {nameof(image.Rows)} is less than or equal to zero.", nameof(image));

            var str = Encoding.UTF8.GetBytes(path);

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.save_dng(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");
        }

        public static void SaveJpeg(Array2DBase image, string path, int quality = 75)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (image.Rows <= 0 || image.Columns <= 0)
                throw new ArgumentException($"{nameof(image.Columns)} and {nameof(image.Rows)} is less than or equal to zero.", nameof(image));
            if (quality < 0)
                throw new ArgumentOutOfRangeException(nameof(quality), $"{nameof(quality)} is less than zero.");
            if (quality > 100)
                throw new ArgumentOutOfRangeException(nameof(quality), $"{nameof(quality)} is greater than 100.");

            var str = Encoding.UTF8.GetBytes(path);

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.save_jpeg(array2DType, image.NativePtr, str, quality);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");
        }

        public static void SavePng(Array2DBase image, string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (image.Rows <= 0 || image.Columns <= 0)
                throw new ArgumentException($"{nameof(image.Columns)} and {nameof(image.Rows)} is less than or equal to zero.", nameof(image));

            var str = Encoding.UTF8.GetBytes(path);

            var array2DType = image.ImageType.ToNativeArray2DType();
            var ret = Native.save_png(array2DType, image.NativePtr, str);
            if (ret == Native.ErrorType.ArrayTypeNotSupport)
                throw new ArgumentException($"{image.ImageType} is not supported.");
        }

        #endregion

        internal sealed partial class Native
        {

            internal enum Array2DType
            {

                UInt8 = 0,

                UInt16,

                Int16,

                Int32,

                Float,

                Double,

                RgbPixel,

                RgbAlphaPixel,

                HsiPixel,

                Matrix

            }

            internal enum ElementType
            {

                OpHeatmap,

                OpJet,

                OpArray2DToMat,

                OpTrans

            }

            internal enum MatrixElementType
            {

                UInt8 = 0,

                UInt16,

                UInt32,

                Int8,

                Int16,

                Int32,

                Float,

                Double,

                RgbPixel,

                RgbAlphaPixel,

                HsiPixel

            }

            internal enum InterpolationTypes
            {

                NearestNeighbor = 0,

                Bilinear,

                Quadratic

            }

            internal enum PointMappingTypes
            {

                Rotator = 0,

                Transform,

                TransformAffine,

                TransformProjective

            }

            internal enum MlpKernelType
            {

                Kernel1 = 0

            }

            internal enum ErrorType
            {

                OK = 0,

                ArrayTypeNotSupport = -1,

                InputArrayTypeNotSupport = -2,

                OutputArrayTypeNotSupport = -3,

                ElementTypeNotSupport = -4,

                InputElementTypeNotSupport = -5,

                OutputElementTypeNotSupport = -6,

                MatrixElementTypeNotSupport = -7,

                //InputOutputArrayNotSameSize = -8,

                //InputOutputMatrixNotSameSize = -9

                MlpKernelNotSupport = -8

            }

            #region array

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array_new(Array2DType type);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array_new1(Array2DType type, uint newSize);

            #region array2d

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array_array2d_new(Array2DType type);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array_array2d_new1(Array2DType type, uint newSize);

            #endregion

            #region matrix

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array_matrix_new(MatrixElementType type);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array_matrix_new1(MatrixElementType type, uint newSize);

            #endregion

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array_delete(IntPtr point);

            #endregion

            #region array2d

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array2d_new(Array2DType type);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array2d_new1(Array2DType type, int rows, int cols);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_delete(Array2DType type, IntPtr array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_nc(Array2DType type, IntPtr array, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_nr(Array2DType type, IntPtr array, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_size(Array2DType type, IntPtr array, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType rectangle_get_rect(Array2DType type, IntPtr array, out IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType array2d_row(Array2DType type, IntPtr array, int row, out IntPtr ret);

            #region array2d_get_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_uint8_t(IntPtr row, int column, out byte value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_uint16_t(IntPtr row, int column, out ushort value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_int16_t(IntPtr row, int column, out short value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_int32_t(IntPtr row, int column, out int value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_double(IntPtr row, int column, out double value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_float(IntPtr row, int column, out float value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_rgb_pixel(IntPtr row, int column, out RgbPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_rgb_alpha_pixel(IntPtr row, int column, out RgbAlphaPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_get_row_column_hsi_pixel(IntPtr row, int column, out HsiPixel value);

            #endregion

            #region array2d_set_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_uint8_t(IntPtr row, int column, byte value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_uint16_t(IntPtr row, int column, ushort value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_int16_t(IntPtr row, int column, short value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_int32_t(IntPtr row, int column, int value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_double(IntPtr row, int column, double value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_float(IntPtr row, int column, float value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_rgb_pixel(IntPtr row, int column, RgbPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_rgb_alpha_pixel(IntPtr row, int column, RgbAlphaPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_set_row_column_hsi_pixel(IntPtr row, int column, HsiPixel value);

            #endregion

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_row_delete(Array2DType type, IntPtr row);

            #region array2d_matrix

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array2d_matrix_new(MatrixElementType type);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array2d_matrix_new1(MatrixElementType type, int rows, int cols);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_delete(MatrixElementType type, IntPtr array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_matrix_nc(MatrixElementType type, IntPtr array, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_matrix_nr(MatrixElementType type, IntPtr array, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_matrix_size(MatrixElementType type, IntPtr matrix, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType rectangle_get_rect2(MatrixElementType type, IntPtr matrix, out IntPtr rect);

            #endregion

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType array2d_matrix_row(MatrixElementType type, IntPtr array, int row, out IntPtr ret);

            #region array2d_matrix_get_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_uint8_t(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_uint16_t(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_uint32_t(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_int8_t(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_int16_t(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_int32_t(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_double(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_float(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_rgb_pixel(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_rgb_alpha_pixel(IntPtr row, int column, out IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_get_row_column_hsi_pixel(IntPtr row, int column, out IntPtr value);

            #endregion

            #region array2d_matrix_set_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_uint8_t(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_uint16_t(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_uint32_t(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_int8_t(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_int16_t(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_int32_t(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_double(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_float(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_rgb_pixel(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_rgb_alpha_pixel(IntPtr row, int column, IntPtr value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_set_row_column_hsi_pixel(IntPtr row, int column, IntPtr value);

            #endregion

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_matrix_row_delete(MatrixElementType type, IntPtr row);

            #region array2d_fhog_matrix

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array2d_fhog_matrix_new(MatrixElementType type);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr array2d_fhog_matrix_new1(MatrixElementType type, int rows, int cols);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void array2d_fhog_matrix_delete(MatrixElementType type, IntPtr array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_fhog_matrix_nc(MatrixElementType type, IntPtr array, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_fhog_matrix_nr(MatrixElementType type, IntPtr array, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool array2d_fhog_matrix_size(MatrixElementType type, IntPtr matrix, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType array2d_fhog_matrix_get_rect2(MatrixElementType type, IntPtr matrix, out IntPtr rect);

            #endregion

            #endregion

            #region load_bmp

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType load_bmp(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region load_dng

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType load_dng(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region load_image

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType load_image(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region load_jpeg

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType load_jpeg(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region load_png

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType load_png(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region save_bmp

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType save_bmp(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region save_dng

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType save_dng(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region save_jpeg

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType save_jpeg(Array2DType type, IntPtr array, byte[] path, int quality);

            #endregion

            #region save_png

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType save_png(Array2DType type, IntPtr array, byte[] path);

            #endregion

            #region matrix

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr matrix_new(MatrixElementType matrixElementType);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr matrix_new1(MatrixElementType matrixElementType, int row, int column);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_delete(MatrixElementType matrixElementType, IntPtr array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_nc(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_nr(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, byte[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, ushort[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, uint[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, sbyte[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, short[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, int[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, float[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, double[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, RgbPixel[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, RgbAlphaPixel[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int matrix_operator_array(MatrixElementType type, IntPtr matrix, HsiPixel[] array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType matrix_operator_left_shift(MatrixElementType type, IntPtr matrix, IntPtr ofstream);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_size(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

            #region matrix_operator_get_one_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_uint8_t(IntPtr matrix, int index, out byte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_uint16_t(IntPtr matrix, int index, out ushort ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_uint32_t(IntPtr matrix, int index, out uint ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_int8_t(IntPtr matrix, int index, out sbyte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_int16_t(IntPtr matrix, int index, out short ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_int32_t(IntPtr matrix, int index, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_double(IntPtr matrix, int index, out double ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_float(IntPtr matrix, int index, out float ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_rgb_pixel(IntPtr matrix, int index, out RgbPixel ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_rgb_alpha_pixel(IntPtr matrix, int index, out RgbAlphaPixel ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_one_row_column_hsi_pixel(IntPtr matrix, int index, out HsiPixel ret);

            #endregion

            #region matrix_operator_set_one_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_uint8_t(IntPtr matrix, int index, byte value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_uint16_t(IntPtr matrix, int index, ushort value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_uint32_t(IntPtr matrix, int index, uint value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_int8_t(IntPtr matrix, int index, sbyte value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_int16_t(IntPtr matrix, int index, short value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_int32_t(IntPtr matrix, int index, int value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_double(IntPtr matrix, int index, double value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_float(IntPtr matrix, int index, float value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_rgb_pixel(IntPtr matrix, int index, RgbPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_rgb_alpha_pixel(IntPtr matrix, int index, RgbAlphaPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_one_row_column_hsi_pixel(IntPtr matrix, int index, HsiPixel value);

            #endregion

            #region matrix_operator_get_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_uint8_t(IntPtr matrix, int row, int column, out byte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_uint16_t(IntPtr matrix, int row, int column, out ushort ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_uint32_t(IntPtr matrix, int row, int column, out uint ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_int8_t(IntPtr matrix, int row, int column, out sbyte ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_int16_t(IntPtr matrix, int row, int column, out short ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_int32_t(IntPtr matrix, int row, int column, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_double(IntPtr matrix, int row, int column, out double ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_float(IntPtr matrix, int row, int column, out float ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_rgb_pixel(IntPtr matrix, int row, int column, out RgbPixel ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_rgb_alpha_pixel(IntPtr matrix, int row, int column, out RgbAlphaPixel ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_get_row_column_hsi_pixel(IntPtr matrix, int row, int column, out HsiPixel ret);

            #endregion

            #region matrix_operator_set_row_column

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_uint8_t(IntPtr matrix, int row, int column, byte value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_uint16_t(IntPtr matrix, int row, int column, ushort value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_uint32_t(IntPtr matrix, int row, int column, uint value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_int8_t(IntPtr matrix, int row, int column, sbyte value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_int16_t(IntPtr matrix, int row, int column, short value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_int32_t(IntPtr matrix, int row, int column, int value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_double(IntPtr matrix, int row, int column, double value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_float(IntPtr matrix, int row, int column, float value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_rgb_pixel(IntPtr matrix, int row, int column, RgbPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_rgb_alpha_pixel(IntPtr matrix, int row, int column, RgbAlphaPixel value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_operator_set_row_column_hsi_pixel(IntPtr matrix, int row, int column, HsiPixel value);

            #endregion

            #endregion

            #region matrix_range_exp

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct matrix_range_exp_create_param
            {
                // uint8_t
                public uint8_t uint8_t_start;
                public uint8_t uint8_t_inc;
                public uint8_t uint8_t_end;
                public bool use_uint8_t_inc;

                // uint16_t
                public uint16_t uint16_t_start;
                public uint16_t uint16_t_inc;
                public uint16_t uint16_t_end;
                bool use_uint16_t_inc;

                // int8_t
                public int8_t int8_t_start;
                public int8_t int8_t_inc;
                public int8_t int8_t_end;
                bool use_int8_t_inc;

                // int16_t
                public int16_t int16_t_start;
                public int16_t int16_t_inc;
                public int16_t int16_t_end;
                bool use_int16_t_inc;

                // int32_t
                public int32_t int32_t_start;
                public int32_t int32_t_inc;
                public int32_t int32_t_end;
                public bool use_int32_t_inc;

                // float
                public float float_start;
                public float float_inc;
                public float float_end;
                public bool use_float_inc;

                // double
                public double double_start;
                public double double_inc;
                public double double_end;
                public bool use_double_inc;

                public bool use_num;
                public int num;
            }

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr matrix_range_exp_create(MatrixElementType matrixElementType, ref matrix_range_exp_create_param param);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void matrix_range_exp_delete(IntPtr array);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_range_exp_nc(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool matrix_range_exp_nr(MatrixElementType matrixElementType, IntPtr matrix, out int ret);

            #endregion

            #region mlp

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr mlp_kernel_new(MlpKernelType kernel_type, int nodes_in_input_layer, int nodes_in_first_hidden_layer, int nodes_in_second_hidden_layer, int nodes_in_output_layer, double alpha, double momentum);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType mlp_kernel_train(MlpKernelType kernel_type, IntPtr kernel, MatrixElementType matrixElementType, IntPtr example_in, double example_out);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ErrorType mlp_kernel_operator(MlpKernelType kernel_type, IntPtr kernel, MatrixElementType type, IntPtr data, out IntPtr ret_mat);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void mlp_kernel_delete(MlpKernelType kernel_type, IntPtr kernel);

            #endregion

            #region vector_matrix

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_matrix_new1(MatrixElementType matrixElementType);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_matrix_new2(MatrixElementType matrixElementType, IntPtr size);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_matrix_new3([In] MatrixElementType matrixElementType, [In] IntPtr[] data, IntPtr dataLength);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_matrix_getSize(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_matrix_getPointer(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_matrix_at(IntPtr vector, int index);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_matrix_delete(MatrixElementType matrixElementType, IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_matrix_copy(IntPtr vector, IntPtr[] dst);

            #endregion

        }

    }

}