#include <Python.h>
#include <stdbool.h>

// klaidu objektai
static PyObject *PrimeError;
static PyObject *NegativeValueError;
static PyObject *NotIntegerError;

typedef struct {
    PyObject_HEAD
    PyObject *limit; /* riba */
} Prime;

// __init__
static int
Prime_init(Prime *self, PyObject *args, PyObject *kwds)
{
    PyObject *limit=NULL, *tmp;

    if (!PyArg_ParseTuple(args, "O", &limit)) {
        return -1;
    }

    if (!PyLong_Check(limit)) {
        PyErr_SetString(NotIntegerError, "The limit attribute value must be an integer");
        return -1;
    }

    if (PyLong_AsLong(limit) < 0) {
        PyErr_SetString(NegativeValueError, "Limit must be greater than zero");
        return -1;
    }

    tmp = self->limit;
    Py_INCREF(limit);
    self->limit = limit;
    Py_XDECREF(tmp);

    return 0;
}

// getter skaiciui
static PyObject *
Prime_getlimit(Prime *self, void *closure)
{
    Py_INCREF(self->limit);
    return self->limit;
}

// setter skaiciui
static int
Prime_setlimit(Prime *self, PyObject *value, void *closure)
{
    if (value == NULL) {
        PyErr_SetString(PrimeError, "Cannot delete the limit attribute");
        return -1;
    }

    if (!PyLong_Check(value)) {
        PyErr_SetString(NotIntegerError, "The limit attribute value must be an integer");
        return -1;
    }

    long limit_value = PyLong_AsLong(value);
    if (limit_value <= 0) {
        PyErr_SetString(NegativeValueError, "Limit must be greater than zero");
        return -1;
    }

    Py_INCREF(value);
    Py_XDECREF(self->limit);
    self->limit = value;

    return 0;
}

// C funkcija tikrinanti ar pirminis skaicius
bool is_prime_c(int n) {
    if (n <= 1) return false;
    if (n <= 3) return true;

    if (n % 2 == 0 || n % 3 == 0) return false;

    for (int i = 5; i * i <= n; i += 6) {
        if (n % i == 0 || n % (i + 2) == 0) return false;
    }

    return true;
}

// metodas patikrinantis ar ribos skaicius pirminis su C funkcija
static PyObject* is_prime(Prime* self) {

    long n = PyLong_AsLong(self->limit);
    if (is_prime_c(n)) {
        Py_RETURN_TRUE;
    } else {
        Py_RETURN_FALSE;
    }
}

// metodas naudojantis is_prime is pythono ir randantis pirminius iki nurodytos ribos
static PyObject *
Prime_get_primes(Prime *self)
{
    PyObject *list = PyList_New(0);
    PyObject *num, *result;
    PyObject *is_prime_func, *is_prime_args;

    PyObject *module = PyImport_ImportModule("prime_checker");
    if (module == NULL) return NULL;

    is_prime_func = PyObject_GetAttrString(module, "is_prime");
    if (is_prime_func == NULL) return NULL;

    for (long i = 2; i <= PyLong_AsLong(self->limit); ++i)
    {
        num = PyLong_FromLong(i);
        is_prime_args = PyTuple_Pack(1, num);
        result = PyObject_CallObject(is_prime_func, is_prime_args);

        if (result == NULL) {
            Py_DECREF(num);
            Py_DECREF(is_prime_args);
            return NULL;
        }

        if (PyObject_IsTrue(result)) {
            PyList_Append(list, num);
        }

        Py_DECREF(num);
        Py_DECREF(is_prime_args);
        Py_DECREF(result);
    }

    Py_DECREF(is_prime_func);
    Py_DECREF(module);

    return list;
}

// metodai
static PyMethodDef Prime_methods[] = {
    {"get_primes", (PyCFunction)Prime_get_primes, METH_NOARGS,
     "Return primes up to the limit"
    },
    {"is_prime", (PyCFunction)is_prime, METH_NOARGS,
     "Return if the limit is prime"
    },
    {NULL}  /* Sentinel */
};

// atributai
static PyGetSetDef Prime_getseters[] = {
    {"limit", 
     (getter)Prime_getlimit, (setter)Prime_setlimit,
     "upper limit for primes", 
     NULL},
    {NULL}  /* Sentinel */
};

// objekto sunaikinimas
static void
Prime_dealloc(Prime* self)
{
    Py_XDECREF(self->limit);
    Py_TYPE(self)->tp_free((PyObject*)self);
}

// Prime tipo objektas
static PyTypeObject PrimeType = {
    PyVarObject_HEAD_INIT(NULL, 0)
    .tp_name = "prime_extent.Prime",
    .tp_doc = "Prime objects",
    .tp_basicsize = sizeof(Prime),
    .tp_itemsize = 0,
    .tp_flags = Py_TPFLAGS_DEFAULT | Py_TPFLAGS_BASETYPE,
    .tp_new = PyType_GenericNew,
    .tp_init = (initproc) Prime_init,
    .tp_dealloc = (destructor) Prime_dealloc,
    .tp_methods = Prime_methods,
    .tp_getset = Prime_getseters,
};

// Modulio sukurimas
static PyModuleDef primemodule = {
    PyModuleDef_HEAD_INIT,
    .m_name = "prime_extent",
    .m_doc = "Finding primes till given number",
    .m_size = -1,
};

// klaidu ir objektu sukurimas
PyMODINIT_FUNC
PyInit_prime_extent(void)
{
    PyObject* m;

    if (PyType_Ready(&PrimeType) < 0)
        return NULL;

    m = PyModule_Create(&primemodule);
    if (m == NULL)
        return NULL;

    Py_INCREF(&PrimeType);
    if (PyModule_AddObject(m, "Prime", (PyObject *) &PrimeType) < 0) {
        Py_DECREF(&PrimeType);
        Py_DECREF(m);
        return NULL;
    }

    PrimeError = PyErr_NewException("prime_extent.error", NULL, NULL);
    Py_INCREF(PrimeError);
    PyModule_AddObject(m, "error", PrimeError);

    NegativeValueError = PyErr_NewException("prime_extent.negative_value_error", NULL, NULL);
    Py_INCREF(NegativeValueError);
    PyModule_AddObject(m, "negative_value_error", NegativeValueError);

    NotIntegerError = PyErr_NewException("prime_extent.not_integer_error", NULL, NULL);
    Py_INCREF(NotIntegerError);
    PyModule_AddObject(m, "not_integer_error", NotIntegerError);

    return m;
}

