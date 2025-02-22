using UnityEditor;
using UnityEngine;

namespace Jerbo.Inspector {
    [CustomPropertyDrawer(typeof(Tab))]
    public class TabDrawer : DecoratorDrawer {
        readonly Color tintColor = new Color32(160, 160, 160, 255);

        const int ACCENT_STRIPE_HEIGHT_OFFSET = 9;
        const int ACCENT_STRIPE_HEIGHT = 6;

        const int TAB_ICON_SIZE = 32;

        const int TAB_HEIGHT = 16;
        const int TAB_PADDING_ABOVE = 18;
        const int TAB_PADDING_BELOW = 8;

        const int TEXT_PADDING_WIDTH = 8;
        const float HEADER_X_OFFSET = 2;


        readonly GUIStyle headerStyle = new () {
            normal = new GUIStyleState() { textColor = new Color32(200, 200, 200, 255) },
            padding = new RectOffset(TEXT_PADDING_WIDTH, TEXT_PADDING_WIDTH, 0, 0),
            alignment = TextAnchor.MiddleLeft,
            fontSize = 14,
            fontStyle = FontStyle.Bold
        };

        public override float GetHeight() {
            return TAB_HEIGHT + TAB_PADDING_ABOVE + TAB_PADDING_BELOW;
        }

        public override void OnGUI(Rect rect) {
            Tab tabProperty = (Tab)attribute;
            if (tabProperty == null) return;

            float xPos = rect.position.x;
            float yPos = rect.position.y + TAB_PADDING_ABOVE;
            float fullWidth = rect.width;


            // Tab Icon
            Matrix4x4 baseMatrix = GUI.matrix;
            Vector2 rectPos = new Vector2(xPos - TEXT_PADDING_WIDTH, yPos);
            Rect colorTabRect = new (rectPos.x, rectPos.y, TAB_ICON_SIZE*2, TAB_ICON_SIZE);
            Color editorColor = tabProperty.color * new Color32(56, 56, 56, 255);

            const int ICON_SEGMENTS = 3;
            const int ICON_FILL_STEPS = 6;
            float distance = TAB_ICON_SIZE * 0.75f;

#if true
            // GUIUtility.RotateAroundPivot(45, rectPos + new Vector2(TAB_ICON_SIZE, TAB_ICON_SIZE) * 0.5f);
            // GUIUtility.RotateAroundPivot(-45, new Vector2(colorTabRect.center.x, colorTabRect.yMax));
            // EditorGUI.DrawRect(colorTabRect, Color.white);
            // GUIUtility.RotateAroundPivot(45, rectPos + new Vector2(TAB_ICON_SIZE,-TAB_ICON_SIZE*0.5f) * 0.5f);
            // EditorGUI.DrawRect(colorTabRect, Color.yellow);
            // GUIUtility.RotateAroundPivot(-45, rectPos + new Vector2(TAB_ICON_SIZE-3,-TAB_ICON_SIZE*0.5f) * 0.5f);
            // EditorGUI.DrawRect(colorTabRect, Color.red);
            //
            // for (int i = 0; i < ICON_SEGMENTS; i++) {
            //     Color c = Color.Lerp(editorColor, tabProperty.color, Mathf.InverseLerp(-1, ICON_SEGMENTS - 1, i));
            //     // float angle = Mathf.Lerp(-45, 45, Mathf.InverseLerp(0, ICON_SEGMENTS - 1, i));
            //     GUIUtility.RotateAroundPivot(90f / (ICON_SEGMENTS-1), new Vector2(colorTabRect.center.x, colorTabRect.yMax));
            //     EditorGUI.DrawRect(colorTabRect, c);
            // }
            //
            
            
            EditorGUI.DrawRect(colorTabRect, Color.yellow);
            Rect smallBlue = new Rect(colorTabRect.min, new Vector2(TAB_ICON_SIZE - 12, TAB_ICON_SIZE * 0.5f - 3));
            EditorGUI.DrawRect(smallBlue, Color.blue);
            smallBlue.y += TAB_ICON_SIZE*0.5f + 3;
            EditorGUI.DrawRect(smallBlue, Color.blue);
            Rect bigblue = new Rect(colorTabRect.min + new Vector2(TAB_ICON_SIZE - 6, 0),
                new Vector2(TAB_ICON_SIZE+6, smallBlue.height));
            EditorGUI.DrawRect(bigblue, Color.blue);
            bigblue.y += TAB_ICON_SIZE*0.5f + 3;
            EditorGUI.DrawRect(bigblue, Color.blue);
            
#else
            for (int i = 0; i < ICON_SEGMENTS; i++) {
                Color c = Color.Lerp(editorColor, tabProperty.color, Mathf.InverseLerp(-1, ICON_SEGMENTS - 1, i));
                float dstStart = distance / (ICON_SEGMENTS - 1) * i;
                float dstEnd = distance / (ICON_SEGMENTS - 1) * (i + 1);

                for (int k = 0; k < ICON_FILL_STEPS; k++) {
                    float t = Mathf.Lerp(dstStart, dstEnd, k / (ICON_FILL_STEPS - 1f));
                    Quaternion rot = Quaternion.LookRotation(Vector3.forward,
                        Quaternion.Euler(0, 0, 45) * new Vector3(0, 1, 0));
                    Matrix4x4 tr = Matrix4x4.Translate(colorTabRect.center - new Vector2(-t, t * 0.45f));
                    GUI.matrix = baseMatrix * tr * Matrix4x4.Rotate(rot) * Matrix4x4.Inverse(tr);

                    EditorGUI.DrawRect(colorTabRect, c);
                }
            }
#endif

            GUI.matrix = baseMatrix;


            // Title
            GUIContent titleContent = new (tabProperty.title);
            float titleWidth = headerStyle.CalcSize(titleContent).x;
            Rect textTitleRect = new Rect(xPos + HEADER_X_OFFSET + TAB_ICON_SIZE, yPos, fullWidth - HEADER_X_OFFSET, TAB_HEIGHT);
            EditorGUI.LabelField(textTitleRect, titleContent, headerStyle);


            // Accent stripe
            Rect colorStripeRect = new Rect(xPos + titleWidth + TAB_ICON_SIZE, textTitleRect.yMax - 2 - ACCENT_STRIPE_HEIGHT,
                fullWidth - titleWidth - TAB_ICON_SIZE, ACCENT_STRIPE_HEIGHT);
            EditorGUI.DrawRect(colorStripeRect, tabProperty.color * tintColor);
        }
    }
}
