
#ifndef ASTYLE_EXPORT_H
#define ASTYLE_EXPORT_H

//-----------------------------------------------------------------------------
// declarations
//-----------------------------------------------------------------------------

#ifdef ASTYLE_LIB

	// define STDCALL and EXPORT for Windows
	// MINGW defines STDCALL in Windows.h (actually windef.h)
	// EXPORT has no value if ASTYLE_NO_EXPORT is defined
	#ifdef _WIN32
		#ifndef STDCALL
			#define STDCALL __stdcall
		#endif
		// define this to prevent compiler warning and error messages
		#ifdef ASTYLE_NO_EXPORT
			#define EXPORT
		#else
			#define EXPORT __declspec(dllexport)
		#endif
		// define STDCALL and EXPORT for non-Windows
		// visibility attribute allows "-fvisibility=hidden" compiler option
	#else
		#define STDCALL
		#if __GNUC__ >= 4
			#define EXPORT __attribute__ ((visibility ("default")))
		#else
			#define EXPORT
		#endif
	#endif	// #ifdef _WIN32

#else
	#define EXPORT
#endif  // #ifdef ASTYLE_LIB

//-----------------------------------------------------------------------------

#endif // closes ASTYLE_EXPORT_H
