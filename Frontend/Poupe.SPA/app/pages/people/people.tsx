import { useEffect, useState } from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TablePagination,
  Paper,
  TableFooter,
  Box,
  Card,
  CardContent,
  Typography,
  Grid,
  IconButton,
} from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import theme from "~/theme/theme";
import AddButton from "~/components/buttons/AddButton";
import AddPeople from "./people.add";
import DeletePeople from "./people.delete";

type HomeRow = {
    id: string;
  name: string;
  age: number;
  receitas: number;
  despesas: number;
  saldo: number;
};

const MOCK_ROWS: HomeRow[] = [
  {
    id: crypto.randomUUID(),
    name: "Ana Silva",
    age: 29,
    receitas: 4500,
    despesas: 2800,
    saldo: 1700,
  },
  {
    id: crypto.randomUUID(),
    name: "Bruno Costa",
    age: 17,
    receitas: 0,
    despesas: 600,
    saldo: -600,
  }
];


export default function PeoplePage() {
  const [openAddPeople, setOpenAddPeople] = useState(false);
  const [openDeletePeople, setOpenDeletePeople] = useState(false);
  const [selectedPeopleId, setSelectedPeopleId] = useState<string | null>(null);

  const [rows, setRows] = useState<HomeRow[]>([]);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [rowsTotal, setRowsTotal] = useState(0);

  const handleDeletePeople = (id: string) => {
    setSelectedPeopleId(id);
    setOpenDeletePeople(true);
  };

  const handleChangePage = (_event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  useEffect(() => {
    setRows(MOCK_ROWS);
    setRowsTotal(MOCK_ROWS.length);
  }, []);

  return (
    <>
      <AddPeople open={openAddPeople} onClose={() => setOpenAddPeople(false)} />
      <DeletePeople
        open={openDeletePeople}
        onClose={() => setOpenDeletePeople(false)}
        id={selectedPeopleId!}
      />
      <Grid
        container
        spacing={2}
        sx={{
          width: "100%",
          marginTop: "1rem",
          justifyContent: "space-around",
        }}
      >
        <Box
          sx={{
            width: "20%",
          }}
        >
          <Card sx={{ borderRadius: "1rem", boxShadow: theme.shadows[3] }}>
            <CardContent>
              <Typography variant="h6" align="center">
                Receitas totais
              </Typography>
              <Typography
                variant="h6"
                align="center"
                sx={{
                  fontWeight: 600,
                  color: theme.palette.success.main,
                }}
              >
                R$ 1500,00
              </Typography>
            </CardContent>
          </Card>
        </Box>
        <Box
          sx={{
            width: "20%",
          }}
        >
          <Card sx={{ borderRadius: "1rem", boxShadow: theme.shadows[3] }}>
            <CardContent>
              <Typography variant="h6" align="center">
                Despesas totais
              </Typography>
              <Typography
                variant="h6"
                align="center"
                sx={{
                  fontWeight: 600,
                  color: theme.palette.error.main,
                }}
              >
                R$ 1500,00
              </Typography>
            </CardContent>
          </Card>
        </Box>
        <Box
          sx={{
            width: "20%",
          }}
        >
          <Card sx={{ borderRadius: "1rem", boxShadow: theme.shadows[3] }}>
            <CardContent>
              <Typography variant="h6" align="center">
                Saldo líquido
              </Typography>
              <Typography
                variant="h6"
                align="center"
                sx={{
                  fontWeight: 600,
                  color: theme.palette.success.main,
                }}
              >
                R$ 1500,00
              </Typography>
            </CardContent>
          </Card>
        </Box>
        <Box
          sx={{
            width: "10%",
            justifyItems: "center",
            alignItems: "center",
            display: "flex",
          }}
        >
          <AddButton onClick={() => setOpenAddPeople(true)} />
        </Box>
      </Grid>
      <Box
        sx={{
          width: "80%",
          marginTop: "2rem",
          marginLeft: "auto",
          marginRight: "auto",
        }}
      >
        <TableContainer
          component={Paper}
          sx={{ borderRadius: "1rem", boxShadow: theme.shadows[3] }}
        >
          <Table aria-label="agents table">
            <TableHead>
              <TableRow>
                <TableCell sx={{ textAlign: "center" }}>Id</TableCell>
                <TableCell sx={{ textAlign: "center" }}>Nome</TableCell>
                <TableCell sx={{ textAlign: "center" }}>Idade</TableCell>
                <TableCell sx={{ textAlign: "center" }}>Receitas</TableCell>
                <TableCell sx={{ textAlign: "center" }}>Despesas</TableCell>
                <TableCell sx={{ textAlign: "center" }}>Saldo</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {rowsTotal === 0 ? (
                <TableRow>
                  <TableCell colSpan={6} align="center">
                    Nenhum resultado encontrado
                  </TableCell>
                </TableRow>
              ) : (
                rows
                  .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                  .map((row, index) => (
                    <TableRow
                      key={index}
                      sx={{
                        cursor: "pointer",
                        "&:hover": {
                          backgroundColor: "action.hover",
                        },
                      }}
                    >
                      <TableCell align="center">{row.id}</TableCell>
                      <TableCell align="center">{row.name}</TableCell>
                      <TableCell align="center">{row.age}</TableCell>
                      <TableCell
                        align="center"
                        sx={{ color: theme.palette.success.main }}
                      >
                        {row.receitas.toLocaleString("pt-BR", {
                          style: "currency",
                          currency: "BRL",
                        })}
                      </TableCell>
                      <TableCell
                        align="center"
                        sx={{ color: theme.palette.error.main }}
                      >
                        {row.despesas.toLocaleString("pt-BR", {
                          style: "currency",
                          currency: "BRL",
                        })}
                      </TableCell>
                      <TableCell
                        align="center"
                        sx={{
                          fontWeight: 600,
                          color:
                            row.saldo >= 0
                              ? theme.palette.success.main
                              : theme.palette.error.main,
                        }}
                      >
                        {row.saldo.toLocaleString("pt-BR", {
                          style: "currency",
                          currency: "BRL",
                        })}
                      </TableCell>
                      <TableCell>
                        <IconButton
                          onClick={() => handleDeletePeople(row.id)}
                          aria-label="Deletar"
                          title="Deletar"
                        >
                          <DeleteIcon color="error" />
                        </IconButton>
                      </TableCell>
                    </TableRow>
                  ))
              )}
            </TableBody>

            <TableFooter>
              {rows.length === 0 && (
                <TableRow>
                  <TableCell colSpan={3} />
                </TableRow>
              )}
            </TableFooter>
          </Table>
          <TablePagination
            rowsPerPageOptions={[3, 5, 10]}
            component="div"
            count={rowsTotal}
            rowsPerPage={rowsPerPage}
            page={page}
            onPageChange={handleChangePage}
            onRowsPerPageChange={handleChangeRowsPerPage}
            labelRowsPerPage="Resultados por página"
            labelDisplayedRows={({ from, to, count }) =>
              `${from}-${to} de ${count !== -1 ? count : `mais de ${to}`}`
            }
          />
        </TableContainer>
      </Box>
    </>
  );
}
