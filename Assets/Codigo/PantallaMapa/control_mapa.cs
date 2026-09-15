using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ControlMapa : MonoBehaviour
{
    public RectTransform mapa_contenido;
    public RectTransform zona_mapa;

    public float velocidad_zoom = 0.1f;
    public float velocidad_movimiento = 1f;

    public float zoom_minimo = 1f;
    public float zoom_maximo = 3f;

    private Vector2 posicion_anterior;
    private bool moviendo_mapa = false;

    void Update()
    {
        controlar_mouse();
        controlar_touch();
    }

    void controlar_mouse()
    {
        if (Mouse.current == null)
        {
            return;
        }

        //Zoom 
        float rueda = Mouse.current.scroll.ReadValue().y;

        if (rueda != 0)
        {
            float direccion = Mathf.Sign(rueda);

            cambiar_zoom(direccion * velocidad_zoom);
        }

        // Comenzar a mover
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            posicion_anterior = Mouse.current.position.ReadValue();
            moviendo_mapa = true;
        }

        // Mover
        if (Mouse.current.leftButton.isPressed && moviendo_mapa)
        {
            Vector2 posicion_actual = Mouse.current.position.ReadValue();
            Vector2 movimiento = (posicion_actual - posicion_anterior)* velocidad_movimiento;

            mapa_contenido.anchoredPosition += movimiento;

            limitar_posicion();

            posicion_anterior = posicion_actual;
        }

        // Dejar de mover
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            moviendo_mapa = false;
        }
    }

    void controlar_touch()
    {
        if (Touchscreen.current == null)
        {
            return;
        }

        TouchControl dedo1 = null;
        TouchControl dedo2 = null;

        // Buscar los dos dedos que están activos
        foreach (var dedo in Touchscreen.current.touches)
        {
            if (!dedo.press.isPressed)
            {
                continue;
            }

            if (dedo1 == null)
            {
                dedo1 = dedo;
            }
            else if (dedo2 == null)
            {
                dedo2 = dedo;
                break;
            }
        }

        //ZOOM
        if (dedo1 != null && dedo2 != null)
        {
            hacer_zoom_con_dos_dedos(dedo1, dedo2);
        }
        //MOVER MAPA
        else if (dedo1 != null)
        {
            Vector2 movimiento = dedo1.delta.ReadValue();
            mapa_contenido.anchoredPosition += movimiento;

            limitar_posicion();
        }
    }

    void hacer_zoom_con_dos_dedos(TouchControl dedo1,TouchControl dedo2)
    {
        Vector2 posicion1 = dedo1.position.ReadValue();
        Vector2 posicion2 = dedo2.position.ReadValue();
        Vector2 posicion1_anterior = posicion1 - dedo1.delta.ReadValue();
        Vector2 posicion2_anterior = posicion2 - dedo2.delta.ReadValue();

        // Distancia entre los dedos antes y después
        float distancia_anterior = Vector2.Distance(posicion1_anterior, posicion2_anterior);
        float distancia_actual = Vector2.Distance(posicion1, posicion2);
        float diferencia = distancia_actual - distancia_anterior;

        // Punto que queda entre los dos dedos
        Vector2 centro_dedos = (posicion1 + posicion2) / 2f;

        // Convertir el centro de los dedos a coordenadas del mapa
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mapa_contenido, centro_dedos, null, out Vector2 punto_local);

        float escala_anterior = mapa_contenido.localScale.x;
        float nueva_escala = escala_anterior + diferencia * 0.01f;

        nueva_escala = Mathf.Clamp(nueva_escala, zoom_minimo, zoom_maximo);

        if (nueva_escala != escala_anterior)
        {
            mapa_contenido.localScale = new Vector3( nueva_escala, nueva_escala, 1f);

            Vector2 movimiento_zoom = punto_local * (escala_anterior - nueva_escala);

            mapa_contenido.anchoredPosition += movimiento_zoom;
        }
        limitar_posicion();
    }

    void cambiar_zoom(float cantidad)
    {
        float escala_actual =mapa_contenido.localScale.x;
        float nueva_escala = escala_actual + cantidad;

        nueva_escala = Mathf.Clamp(nueva_escala, zoom_minimo, zoom_maximo);

        mapa_contenido.localScale = new Vector3(nueva_escala, nueva_escala, 1f);
        limitar_posicion();
    }

    void limitar_posicion()
    {
        float ancho_mapa = mapa_contenido.rect.width * mapa_contenido.localScale.x;

        float alto_mapa = mapa_contenido.rect.height * mapa_contenido.localScale.y;

        float ancho_zona = zona_mapa.rect.width;

        float alto_zona = zona_mapa.rect.height;

        float limite_x = Mathf.Max(0, (ancho_mapa - ancho_zona) / 2);

        float limite_y = Mathf.Max(0, (alto_mapa - alto_zona) / 2);

        float posicion_x = Mathf.Clamp(mapa_contenido.anchoredPosition.x, -limite_x, limite_x);

        float posicion_y = Mathf.Clamp(mapa_contenido.anchoredPosition.y, -limite_y, limite_y);

        mapa_contenido.anchoredPosition = new Vector2(posicion_x, posicion_y);
    }
}