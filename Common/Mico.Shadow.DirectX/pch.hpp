#pragma once

#ifdef _DEBUG
#define DEBUG_LOG(result,expression) if (FAILED(result)) throw gcnew System::NotImplementedException(expression);
#else 
#define DEBUG_LOG(result,expression)
#endif // _DEBUG
