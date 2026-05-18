cmake_minimum_required(VERSION 3.16)

project(LegacyControlZoo LANGUAGES CXX)

add_executable(LegacyControlZoo WIN32 LegacyControlZoo.cpp)
target_compile_features(LegacyControlZoo PRIVATE cxx_std_17)
target_compile_definitions(LegacyControlZoo PRIVATE UNICODE _UNICODE)

if(MSVC)
  target_compile_options(LegacyControlZoo PRIVATE /W4 /permissive-)
else()
  target_compile_options(LegacyControlZoo PRIVATE -Wall -Wextra)
endif()

target_link_libraries(LegacyControlZoo PRIVATE comctl32 user32 gdi32)
