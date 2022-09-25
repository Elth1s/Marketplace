import { createTheme, hexToRgb } from "@mui/material";

const lightTheme = createTheme({
    breakpoints: {
        values: {
            xs: 0,
            sm: 600,
            md: 900,
            lg: 1185,
            xl: 1560,
        },
    },
    palette: {
        mode: "light",
        primary: {
            main: '#F45626',
            dark: "#CB2525"
        },
        secondary: {
            main: '#0E7C3A',
            light: "#30AA61"
        },
        error: {
            main: '#AF0000',
        },
        common: {
            black: "#000",
            white: "#fff"
        },
        text: {
            primary: "#000"
        },
    },
    components: {
        MuiCssBaseline: {
            styleOverrides: {
                "&::-webkit-scrollbar-corner": {
                    backgroundColor: hexToRgb("#fff")
                },
                "&::-webkit-scrollbar-track": {
                    backgroundColor: hexToRgb("#fff")
                }
            }
        },
        MuiContainer: {
            defaultProps: {
                style: {
                    padding: 0
                }
            }
        },
        MuiIconButton: {
            defaultProps: {
                color: "secondary",
            },
            styleOverrides: {
                root: {
                    "&& .MuiTouchRipple-child": { borderRadius: "12px" }
                }
            }
        },
        MuiAvatar: {
            defaultProps: {
                imgProps: {
                    sx: {
                        objectFit: "scale-down"
                    }
                }
            },
            styleOverrides: {
                root: {
                    borderRadius: "10px"
                }
            }
        },
        MuiTypography: {
            defaultProps: {
                color: "#000"
            },
        },
        MuiSwitch: {
            styleOverrides: {
                switchBase: {
                    color: "#0E7C3A"
                },
            }
        },
        MuiAppBar: {
            defaultProps: {
                color: "inherit"
            }
        }
    },
    typography: {
        h1: {
            fontSize: "30px",
            lineHeight: "38px"
        },
        h2: {
            fontSize: "27px",
            lineHeight: "34px"
        },
        h3: {
            fontSize: "24px",
            lineHeight: "27px"
        },
        h4: {
            fontSize: "20px",
            lineHeight: "25px"
        },
        h5: {
            fontSize: "18px",
            lineHeight: "23px"
        },
        h6: {
            fontSize: "16px",
            lineHeight: "20px"
        },
        subtitle1: {
            fontSize: "14px",
            lineHeight: "18px"
        },
        fontFamily: [
            'Mulish',
            "sans-serif"
        ].join(',')
    },
});

export default lightTheme