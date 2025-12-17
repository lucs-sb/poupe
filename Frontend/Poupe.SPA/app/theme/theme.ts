import { createTheme } from "@mui/material/styles";

const css = {
  bg: "rgb(var(--color-background))",
  fg: "rgb(var(--color-foreground))",

  muted: "rgb(var(--color-muted))",
  mutedFg: "rgb(var(--color-muted-foreground))",
  border: "rgb(var(--color-border))",

  primary: "rgb(var(--color-primary))",
  primaryFg: "rgb(var(--color-primary-foreground))",

  success: "rgb(var(--color-success))",
  successFg: "rgb(var(--color-success-foreground))",

  danger: "rgb(var(--color-danger))",
  dangerFg: "rgb(var(--color-danger-foreground))",

  warning: "rgb(var(--color-warning))",
};

const theme = createTheme({
  palette: {
    mode: "light",

    primary: {
      main: css.primary,
      contrastText: css.primaryFg,
    },

    background: {
      default: css.bg,
      paper: css.bg,
    },

    text: {
      primary: css.fg,
      secondary: css.mutedFg,
    },

    divider: css.border,

    success: {
      main: css.success,
      contrastText: css.successFg,
    },

    error: {
      main: css.danger,
      contrastText: css.dangerFg,
    },

    warning: {
      main: css.warning,
    },
  },

  typography: {
    fontFamily: "var(--font-sans)",
    h6: { fontWeight: 700 },
    button: { textTransform: "none", fontWeight: 600 },
  },

  shape: {
    borderRadius: 12,
  },

  components: {
    MuiCssBaseline: {
      styleOverrides: {
        body: {
          backgroundColor: css.bg,
          color: css.fg,
        },
      },
    },

    MuiPaper: {
      styleOverrides: {
        root: {
          backgroundImage: "none",
          borderColor: css.border,
        },
      },
    },

    MuiTextField: {
      defaultProps: {
        size: "small",
      },
    },

    MuiOutlinedInput: {
      styleOverrides: {
        notchedOutline: {
          borderColor: css.border,
        },
      },
    },

    MuiButton: {
      styleOverrides: {
        root: {
          height: 40,
          borderRadius: 10,
        },
      },
    },
  },
});

export default theme;