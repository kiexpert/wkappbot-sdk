from setuptools import setup, find_packages

setup(
    name="appbot",
    version="0.1.0",
    description="Windows App UI Automation Test Framework",
    author="AppBot",
    packages=find_packages(),
    python_requires=">=3.10",
    install_requires=[
        "pywinauto>=0.6.8",
        "comtypes>=1.2.0",
        "Pillow>=10.0.0",
        "anthropic>=0.30.0",
        "PyYAML>=6.0",
        "pydantic>=2.0",
        "Jinja2>=3.1.0",
        "colorama>=0.4.6",
    ],
    entry_points={
        "console_scripts": [
            "appbot=appbot.cli:main",
        ],
    },
)
