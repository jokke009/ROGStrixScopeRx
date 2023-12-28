using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Defintions
{
    public readonly struct KeyCoords
    {
        public double X { get; init; }
        public double Y { get; init; }
        public double W { get; init; }
        public double H { get; init; }

        public KeyCoords(double x, double y, double w = 40, double h = 40)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }

    }

    public static class KeyMap
    {
        public static Dictionary<ScopeRx, KeyCoords> KeyMappings = new Dictionary<ScopeRx, KeyCoords>
        {
            { ScopeRx.KEY_EN_F1, new KeyCoords(118.53, 40.685) },
            { ScopeRx.KEY_EN_F2, new KeyCoords(158.53, 40.685) },
            { ScopeRx.KEY_EN_F3, new KeyCoords(198.53, 40.685) },
            { ScopeRx.KEY_EN_F4, new KeyCoords(238.53, 40.685) },
            { ScopeRx.KEY_EN_F5, new KeyCoords(298.53, 40.685) },
            { ScopeRx.KEY_EN_F7, new KeyCoords(378.53, 40.685) },
            { ScopeRx.KEY_EN_F8, new KeyCoords(418.53, 40.685) },
            { ScopeRx.KEY_EN_F9, new KeyCoords(478.53, 40.685) },
            { ScopeRx.KEY_EN_F11, new KeyCoords(558.53, 40.685) },
            { ScopeRx.KEY_EN_PAUSE_BREAK, new KeyCoords(738.53, 40.685) },
            { ScopeRx.KEY_EN_INSERT, new KeyCoords(658.53, 100.69) },
            { ScopeRx.KEY_EN_PAGE_UP, new KeyCoords(738.53, 100.69) },
            { ScopeRx.KEY_EN_DELETE, new KeyCoords(658.53, 140.69) },
            { ScopeRx.KEY_EN_PAGE_DOWN, new KeyCoords(738.53, 140.69) },
            { ScopeRx.KEY_EN_LEFT_ARROW, new KeyCoords(658.53003, 260.69) },
            { ScopeRx.KEY_EN_RIGHT_ARROW, new KeyCoords(738.53, 260.69) },
            { ScopeRx.KEY_EN_NUMPAD_TIMES, new KeyCoords(878.53, 100.69) },
            { ScopeRx.KEY_EN_NUMPAD_MINUS, new KeyCoords(918.53, 100.69) },
            { ScopeRx.KEY_EN_NUMPAD_7, new KeyCoords(798.53, 140.69) },
            { ScopeRx.KEY_EN_NUMPAD_9, new KeyCoords(878.53, 140.69) },
            { ScopeRx.KEY_EN_NUMPAD_PLUS, new KeyCoords(918.53, 140.69,40, 80) }, // Height is 80
            { ScopeRx.KEY_EN_NUMPAD_ENTER, new KeyCoords(918.53, 220.69, 40,80) }, // Height is 80
            { ScopeRx.KEY_EN_NUMPAD_4, new KeyCoords(798.53, 180.69) },
            { ScopeRx.KEY_EN_NUMPAD_6, new KeyCoords(878.53, 180.69) },
            { ScopeRx.KEY_EN_NUMPAD_1, new KeyCoords(798.53, 220.69) },
            { ScopeRx.KEY_EN_NUMPAD_2, new KeyCoords(838.53, 220.69) },
            { ScopeRx.KEY_EN_NUMPAD_3, new KeyCoords(878.53, 220.69) },
            { ScopeRx.KEY_EN_NUMPAD_0, new KeyCoords(798.53, 260.69, 80) }, // Width is 80
            { ScopeRx.KEY_EN_NUMPAD_PERIOD, new KeyCoords(878.53, 260.69) },
            { ScopeRx.KEY_EN_BACK_TICK, new KeyCoords(38.526, 100.69) },
            { ScopeRx.KEY_EN_1, new KeyCoords(78.526, 100.69) },
            { ScopeRx.KEY_EN_2, new KeyCoords(118.53, 100.69) },
            { ScopeRx.KEY_EN_3, new KeyCoords(158.53, 100.69) },
            { ScopeRx.KEY_EN_4, new KeyCoords(198.53, 100.69) },
            { ScopeRx.KEY_EN_5, new KeyCoords(238.53, 100.69) },
            { ScopeRx.KEY_EN_6, new KeyCoords(278.53, 100.69) },
            { ScopeRx.KEY_EN_7, new KeyCoords(318.53, 100.69) },
            { ScopeRx.KEY_EN_8, new KeyCoords(358.53, 100.69) },
            { ScopeRx.KEY_EN_9, new KeyCoords(398.53, 100.69) },
            { ScopeRx.KEY_EN_0, new KeyCoords(438.53, 100.69) },
            { ScopeRx.KEY_EN_MINUS, new KeyCoords(478.53, 100.69) },
            { ScopeRx.KEY_EN_EQUALS, new KeyCoords(518.53, 100.69) },
            { ScopeRx.KEY_EN_ANSI_BACK_SLASH, new KeyCoords(558.53, 100.69) },
            { ScopeRx.KEY_EN_BACKSPACE, new KeyCoords(598.53, 100.69) },
            { ScopeRx.KEY_EN_TAB, new KeyCoords(38.526, 140.69, 60) }, // Width is 60
            { ScopeRx.KEY_EN_Q, new KeyCoords(98.526, 140.69) },
            { ScopeRx.KEY_EN_W, new KeyCoords(138.53, 140.69) },
            { ScopeRx.KEY_EN_E, new KeyCoords(178.53, 140.69) },
            { ScopeRx.KEY_EN_R, new KeyCoords(218.53, 140.69) },
            { ScopeRx.KEY_EN_T, new KeyCoords(258.53, 140.69) },
            { ScopeRx.KEY_EN_Y, new KeyCoords(298.53, 140.69) },
            { ScopeRx.KEY_EN_U, new KeyCoords(338.53, 140.69) },
            { ScopeRx.KEY_EN_I, new KeyCoords(378.53, 140.69) },
            { ScopeRx.KEY_EN_O, new KeyCoords(418.53, 140.69) },
            { ScopeRx.KEY_EN_P, new KeyCoords(458.53, 140.69) },
            { ScopeRx.KEY_EN_LEFT_BRACKET, new KeyCoords(498.53, 140.69) },
            { ScopeRx.KEY_EN_RIGHT_BRACKET, new KeyCoords(538.53, 140.69) },
            { ScopeRx.KEY_EN_CAPS_LOCK, new KeyCoords(38.526, 180.69, 60) }, // Width is 60
            { ScopeRx.KEY_EN_A, new KeyCoords(118.53, 180.69) },
            { ScopeRx.KEY_EN_S, new KeyCoords(158.53, 180.69) },
            { ScopeRx.KEY_EN_D, new KeyCoords(198.53, 180.69) },
            { ScopeRx.KEY_EN_F, new KeyCoords(238.53, 180.69) },
            { ScopeRx.KEY_EN_G, new KeyCoords(278.53, 180.69) },
            { ScopeRx.KEY_EN_H, new KeyCoords(318.53, 180.69) },
            { ScopeRx.KEY_EN_J, new KeyCoords(358.53, 180.69) },
            { ScopeRx.KEY_EN_K, new KeyCoords(398.53, 180.69) },
            { ScopeRx.KEY_EN_L, new KeyCoords(438.53, 180.69) },
            { ScopeRx.KEY_EN_SEMICOLON, new KeyCoords(478.53, 180.69) },
            { ScopeRx.KEY_EN_QUOTE, new KeyCoords(518.53, 180.69) },
            { ScopeRx.KEY_EN_Z, new KeyCoords(138.53, 220.69) },
            { ScopeRx.KEY_EN_X, new KeyCoords(178.53, 220.69) },
            { ScopeRx.KEY_EN_C, new KeyCoords(218.53, 220.69) },
            { ScopeRx.KEY_EN_V, new KeyCoords(258.53, 220.69) },
            { ScopeRx.KEY_EN_B, new KeyCoords(298.53, 220.69) },
            { ScopeRx.KEY_EN_N, new KeyCoords(338.53, 220.69) },
            { ScopeRx.KEY_EN_M, new KeyCoords(378.53, 220.69) },
            { ScopeRx.KEY_EN_COMMA, new KeyCoords(418.53, 220.69) },
            { ScopeRx.KEY_EN_PERIOD, new KeyCoords(458.53, 220.69) },
            { ScopeRx.KEY_EN_FORWARD_SLASH, new KeyCoords(498.53, 220.69) },
            { ScopeRx.KEY_EN_RIGHT_SHIFT, new KeyCoords(538.53, 220.69, 100) }, // Width is 100
            { ScopeRx.KEY_EN_LEFT_SHIFT, new KeyCoords(38.526, 220.69, 100) }, // Width is 100
            { ScopeRx.KEY_EN_LEFT_CONTROL, new KeyCoords(38.526, 260.69) },
            { ScopeRx.KEY_EN_ISO_BACK_SLASH, new KeyCoords(138.53, 260.69, 60) }, // Width is 60
            { ScopeRx.KEY_EN_RIGHT_ALT, new KeyCoords(438.53, 260.69, 60) }, // Width is 60
            { ScopeRx.KEY_EN_RIGHT_FUNCTION, new KeyCoords(498.53, 260.69, 60) }, // Width is 60
            { ScopeRx.KEY_EN_MENU, new KeyCoords(558.53, 260.69) },
            { ScopeRx.KEY_EN_RIGHT_CONTROL, new KeyCoords(598.53, 260.69) },
            { ScopeRx.KEY_EN_ESCAPE, new KeyCoords(38.526, 40.685) },
            { ScopeRx.KEY_EN_SCROLL_LOCK, new KeyCoords(698.53, 40.685) },
            { ScopeRx.KEY_EN_PRINT_SCREEN, new KeyCoords(658, 40.685) },
            { ScopeRx.KEY_EN_END, new KeyCoords(698.53, 140.69) },
            { ScopeRx.KEY_EN_F6, new KeyCoords(338.53, 40.685) },
            { ScopeRx.KEY_EN_F10, new KeyCoords(518.53, 40.685) },
            { ScopeRx.KEY_EN_DOWN_ARROW, new KeyCoords(698.53, 260.69) },
            { ScopeRx.KEY_EN_UP_ARROW, new KeyCoords(698.53, 220.69) },
            //{ ScopeRx.KEY_EN_NUMPAD_2, new KeyCoords(838.53, 260.69) },
            { ScopeRx.KEY_EN_NUMPAD_5, new KeyCoords(838.53, 180.69) },
            { ScopeRx.KEY_EN_NUMPAD_8, new KeyCoords(838.53, 140.69) },
            { ScopeRx.KEY_EN_NUMPAD_DIVIDE, new KeyCoords(838.53, 100.69) },
            { ScopeRx.KEY_EN_NUMPAD_LOCK, new KeyCoords(798.53, 100.69) },
            { ScopeRx.KEY_EN_HOME, new KeyCoords(698.53, 100.69) },
            { ScopeRx.KEY_EN_F12, new KeyCoords(598.53007, 40.685) }, // Corrected x position
            { ScopeRx.KEY_EN_SPACE, new KeyCoords(198.53, 260.69,240,40) }, // Manually added
            { ScopeRx.KEY_EN_ANSI_ENTER, new KeyCoords(578.53003,140.69,60,80) } // Manually added
        };
    }
}
