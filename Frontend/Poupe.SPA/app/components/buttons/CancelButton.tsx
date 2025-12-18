import { Button, type ButtonProps } from "@mui/material";

type CancelButtonProps = {
  label?: string;
} & ButtonProps;

export default function CancelButton({
  label = "Cancelar",
  ...props
}: CancelButtonProps) {
  return (
    <Button
      variant="contained"
      color="inherit"
      size="medium"
      aria-label={label}
      {...props}
    >
      {label}
    </Button>
  );
}
